/*
 * Overlay Package Cloner : a tool to quickly create new overlay packages for the Sims 2.
 *
 * Created by Roxane Morin, "Crisps" in the Sims 2 community.
 * https://github.com/RoxaneMorin
 * https://crispsandkerosene.tumblr.com/
 *
 * This code uses libraries by William Howard.
 * https://github.com/whoward69
 * https://www.picknmixmods.com/
 */


using System;
using Sims2Tools.DBPF;
using Sims2Tools.DBPF.Data;
using Sims2Tools.DBPF.Package;
using Sims2Tools.DBPF.SceneGraph.COLL;
using Sims2Tools.DBPF.SceneGraph.IDR;
using Sims2Tools.DBPF.STR;


namespace CrispsOverlayCloner
{
    public class OverlaySourcePackage
    {
        // VARIABLES
        private DBPFFile package;
        private Dictionary<TypeTypeID, List<DBPFEntry>> packageEntriesByType;

        private int totalResourceCount;
        private SortedDictionary<string, int> resourceCountByType;

        private DBPFEntry strEntry;
        private DBPFEntry collEntry;
        private DBPFEntry idrEntry;

        private string internalOverlayName;
        //private List<string> strResourceNames;


        // PROPERTIES
        public string PackagePath => package.PackagePath;
        public string PackageName => package.PackageName;
        public string PackageNameNoExtn => package.PackageNameNoExtn;
        public string PackageDir => package.PackageDir;

        public int TotalResourceCount => totalResourceCount;
        public SortedDictionary<string, int> ResourceCountPerType => resourceCountByType;

        public DBPFEntry StrEntry => strEntry;
        public DBPFEntry CollEntry => collEntry;
        public DBPFEntry IdrEntry => idrEntry;

        public bool IsValidOverlayPackage => (collEntry != null && idrEntry != null);
        public string? InternalOverlayName => internalOverlayName;


        // METHODS FORWARDED FROM PACKAGE
        public List<DBPFEntry> GetEntriesByType(TypeTypeID type) => package.GetEntriesByType(type);
        public DBPFResource GetResourceByEntry(DBPFEntry entry) => package.GetResourceByEntry(entry);
        public void Close() => package.Close();



        // CONSTRUCTOR & SETUP
        public static OverlaySourcePackage Create(string PackageFilepath)
        {
            // Potentially add guardian measures here.
            return new OverlaySourcePackage(PackageFilepath);
        }

        private OverlaySourcePackage(string PackageFilepath)
        {
            package = new DBPFFile(PackageFilepath);


            // Collect general package info.
            totalResourceCount = totalResourceCount = (int)package.ResourceCount;

            var sortedPackageEntries = package.GetAllEntries().GroupBy(entry => entry.TypeID,
                (typeID, entries) => new
                {
                    Key = typeID,
                    Value = entries,
                    Count = entries.Count()
                });

            packageEntriesByType = sortedPackageEntries.ToDictionary(group => group.Key, group => group.Value.ToList());
            resourceCountByType = new SortedDictionary<string, int>(sortedPackageEntries.ToDictionary(group => DBPFData.TypeName(group.Key), group => group.Count),
                                                                            StringComparer.InvariantCultureIgnoreCase);


            // Verify it contains the appropriate Coll and 3IDR resources.
            List<DBPFEntry> targetEntries;

            if (packageEntriesByType.TryGetValue(Coll.TYPE, out targetEntries))
            {
                // We assume a valid overlay package has only the one Coll resource.
                if (targetEntries.Count == 1 && targetEntries[0].GroupID == (TypeGroupID)0x4F184AA9)
                {
                    collEntry = targetEntries[0];
                }
            }

            if (packageEntriesByType.TryGetValue(Idr.TYPE, out targetEntries))
            {
                foreach (DBPFEntry entry in targetEntries)
                {
                    if (entry.GroupID == (TypeGroupID)0x4F184AA9)
                    {
                        idrEntry = entry;
                        break;
                    }
                }
            }


            // Try to fetch the overlay's internal name from the string resource.
            if (IsValidOverlayPackage && packageEntriesByType.TryGetValue(Str.TYPE, out targetEntries))
            {
                // We assume a valid overlay package has only the one string resource.
                if (targetEntries.Count == 1)
                {
                    strEntry = targetEntries[0];

                    Str theStr = (Str)package.GetResourceByEntry(strEntry);
                    List<string> strContents = theStr.LanguageItems(MetaData.Languages.Default).Select(item => item.Title).ToList();

                    internalOverlayName = strContents[0];
                    //strResourceNames = strContents.Skip(1).ToList();
                }
            }
            else
            {
                internalOverlayName = "[none]";
            }
        }
    }
}

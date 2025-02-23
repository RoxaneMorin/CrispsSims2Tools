Both plugins require Blender 2.8 or above. 
They were tested in Blender 2.9, 3.6 and 4.0 on Windows.

Both are found in the Item tab in Object mode.

--------------------------------------------------------

Basic Move Tool
Save a few clicks sitting objects’ asses on the scene origin.

Commands:

-> Sit Selected at Scene Origin
Moves all selected Mesh, Curve and Text/Font objects to the scene’s origin, their lowest points at height zero. The objects’ own origins (pivot points) move with them, but keep their relative positions in object space.

-> Sit Selected & its Origin(s) at Scene Origin
Moves all selected objects to the scene’s origin, their lowest points at height zero. Their origins (pivot points) are also moved there.

! Apply -> Rotation to your objects before use if they’ve been rotated. 
I use bounding box information to calculate objects’ lowest points. Bounding boxes are automatically updated when an object is translated or scaled, but not on rotation.

--------------------------------------------------------

Bone Name Manager
Remove a substring/pattern from bones’ names; save and restore the originals via save file.
The default pattern is  “#\d+$”: “#” followed by numbers at the end of the name string.

I created this one because the GMDC Import/Export plugin requires the bones to be numbered, but that breaks the symmetry mode for weight painting. I wanted an easy alternative to renaming and re-renaming them by hand.

Commands:

-> Use Custom Regex?
If ticked, the plugin will use the specified regular expression instead of its default pattern. Only the first match found in a name will be taken into account.

-> Strip Bone Names
Removes elements matching the pattern from bones’ names. The original names are not saved.

-> Save Bone Names to File
Creates a json file listing and mapping stripped and original bone names to each other. Uses the Save Path. The bones are not renamed.

-> Strip Bone Names and Save to File
Creates a json file listing and mapping stripped and original bone names to each other. Uses the Save Path. Removes elements matching the pattern from bones’ names. 

-> Restore Bone Names from File
From a valid json file, loads the mapping of stripped to original names and renames bones accordingly. Uses the Load Path. Will fail if the current pattern is different from the one used by the file.

# Created by Roxane Morin, "Crisps" in the Sims 2 community.
# https://github.com/RoxaneMorin
# https://crispsandkerosene.tumblr.com/


bl_info = {
    "name": "Crisps's Bone Names Manager",
    "description": "Strip patterns from bone names, and save and load original and altered bone names to a json file.",
    "author": "CrispsandKerosene",
    "version": (0, 1),
    "blender": (2, 80, 0),
    "location": "View3D > Item",
    "category": "Object",
}


import bpy
import os
import re
import json


# GLOBAL PARAMETERS
default_regex = "#\d+$"


## UI

# Make the save bone names button.
class Crisps_save_bone_names_operator(bpy.types.Operator):
    bl_idname = "object.save_bone_names"
    bl_label = "Save Bone Names to File" # default label.
    bl_description = "Saves the mapping of original and altered bone names to a file"

    @classmethod
    def poll(cls, context):
        # The button should only be clickable if the active object is an armature.
        return (bpy.context.active_object and bpy.context.active_object.type == "ARMATURE")
        
    def execute(self, context):
        if context.scene.bone_manager_use_custom_regex:
            regex = context.scene.bone_manager_custom_regex
        else:
            regex = default_regex
        filepath = context.scene.bone_manager_save_path
        save_bone_names(regex, filepath)
        return {'FINISHED'}
    
# Make the strip bone names button.
class Crisps_strip_bone_names_operator(bpy.types.Operator):
    bl_idname = "object.strip_bone_names"
    bl_label = "Strip Bone Names" # default label.
    bl_description = "Removes the target pattern from the bone names"

    @classmethod
    def poll(cls, context):
        # The button should only be clickable if the active object is an armature.
        return (bpy.context.active_object and bpy.context.active_object.type == "ARMATURE")
        
    def execute(self, context):
        if context.scene.bone_manager_use_custom_regex:
            regex = context.scene.bone_manager_custom_regex
        else:
            regex = default_regex
        filepath = context.scene.bone_manager_save_path
        strip_bone_names(regex, filepath)
        return {'FINISHED'}

# Make the save & strip bone names button.
class Crisps_save_strip_bone_names_operator(bpy.types.Operator):
    bl_idname = "object.save_strip_bone_names"
    bl_label = "Save & Strip Bone Names" # default label.
    bl_description = "Removes the target pattern from the bone names, and saves the mapping of original and altered bone names to a file"

    @classmethod
    def poll(cls, context):
        # The button should only be clickable if the active object is an armature.
        return (bpy.context.active_object and bpy.context.active_object.type == "ARMATURE")
        
    def execute(self, context):
        if context.scene.bone_manager_use_custom_regex:
            regex = context.scene.bone_manager_custom_regex
        else:
            regex = default_regex
        filepath = context.scene.bone_manager_save_path
        save_strip_bone_names(regex, filepath)
        return {'FINISHED'}
    
# Make the load bone names button.
class Crisps_load_bone_names_operator(bpy.types.Operator):
    bl_idname = "object.load_bone_names"
    bl_label = "Restore Bone Names from File" # default label.
    bl_description = "Restores original bone names to altered ones from a mapping file"

    @classmethod
    def poll(cls, context):
        # The button should only be clickable if the active object is an armature.
        return (bpy.context.active_object and bpy.context.active_object.type == "ARMATURE")
        
    def execute(self, context):
        if context.scene.bone_manager_use_custom_regex:
            regex = context.scene.bone_manager_custom_regex
        else:
            regex = default_regex
        filepath = context.scene.bone_manager_load_path
        load_bone_names(regex, filepath)
        return {'FINISHED'}


# Make the panel.
class Crisps_bone_name_panel(bpy.types.Panel):
    bl_idname = "OBJECT_PT_Crisps_bone_name_panel"
    bl_label = "Crisps' Bone Name Manager"
    
    bl_space_type = "VIEW_3D"
    bl_region_type = "UI"
    bl_category = "Item"

    def draw(self, context):
        layout = self.layout
        
        # Change the buttons' labels whether an armature is selected.
        if (bpy.context.active_object and bpy.context.active_object.type == "ARMATURE"):
            label_strip = "Strip Bone Names ({0})".format(bpy.context.active_object.name)
            label_save = "Save Bone Names to File ({0})".format(bpy.context.active_object.name)
            label_save_strip = "Strip Bone Names and Save to File ({0})".format(bpy.context.active_object.name)
            label_load = "Restore Bone Names from File ({0})".format(bpy.context.active_object.name)
        else:
            label_strip = "ActiveObject must be an Armature"
            label_save = "ActiveObject must be an Armature"
            label_save_strip = "ActiveObject must be an Armature"
            label_load = "ActiveObject must be an Armature"
            
        # Stripping pattern.
        box_regex = layout.box()
        box_regex.prop(context.scene, "bone_manager_use_custom_regex")
        column_regex = box_regex.column()
        column_regex.prop(context.scene, "bone_manager_custom_regex")
        column_regex.enabled = context.scene.bone_manager_use_custom_regex
        box_regex.operator("object.strip_bone_names", text = label_strip)

        # Save bone names.
        box_save = layout.box()
        box_save.prop(context.scene, "bone_manager_save_path")
        box_save.operator("object.save_bone_names", text = label_save)
        box_save.operator("object.save_strip_bone_names", text = label_save_strip)

        # Restore bone names.
        box_load = layout.box()
        box_load.prop(context.scene, "bone_manager_load_path")
        box_load.operator("object.load_bone_names", text = label_load)
        

# Handle (un)registering.
def register():
    bpy.utils.register_class(Crisps_save_bone_names_operator)
    bpy.utils.register_class(Crisps_strip_bone_names_operator)
    bpy.utils.register_class(Crisps_save_strip_bone_names_operator)
    bpy.utils.register_class(Crisps_load_bone_names_operator)
    bpy.utils.register_class(Crisps_bone_name_panel)

    bpy.types.Scene.bone_manager_use_custom_regex = bpy.props.BoolProperty( name = "Use Custom Regex?", description = "Whether to use a custom pattern, defined as following, in bone name operations")
    bpy.types.Scene.bone_manager_custom_regex = bpy.props.StringProperty(
        name = "Regex to Use",
        description = "The pattern to match and strip from bone names",
        default = default_regex,
        subtype = "NONE")

    bpy.types.Scene.bone_manager_save_path = bpy.props.StringProperty(
        name = "Save Path",
        description = "Where to save the bone names mapping file",
        default = os.path.expanduser("~/boneNames.json"),
        subtype = "FILE_PATH")
        
    bpy.types.Scene.bone_manager_load_path = bpy.props.StringProperty(
        name = "Load Path",
        description = "File from which to read the saved bone name mapping",
        default = os.path.expanduser("~/boneNames.json"),
        subtype = "FILE_PATH")

def unregister():
    bpy.utils.unregister_class(Crisps_save_bone_names_operator)
    bpy.utils.unregister_class(Crisps_strip_bone_names_operator)
    bpy.utils.unregister_class(Crisps_save_strip_bone_names_operator)
    bpy.utils.unregister_class(Crisps_load_bone_names_operator)
    bpy.utils.unregister_class(Crisps_bone_name_panel)
    
    del bpy.types.Scene.bone_manager_use_custom_regex
    del bpy.types.Scene.bone_manager_custom_regex
    del bpy.types.Scene.bone_manager_save_path
    del bpy.types.Scene.bone_manager_load_path

# Main.
if __name__ == "__main__":
    register()
    

    
## FUNCTIONS

def save_bone_names(regex, filepath):
    
    target_object = bpy.context.active_object
    armature = target_object.data
    
    bone_dict = {}
    bone_dict["regex"] = (regex)
    
    for bone in armature.bones:
        #print("\n"+bone.name)
        
        # Match the regex, add the bone and its stripped counterpart to the dictionary.
        match = re.search(regex, bone.name)
        if (match is not None):
            stripped_name = bone.name[0:match.span()[0]] + bone.name[match.span()[1]:] # concatenate so we can handle middle patterns.
            bone_dict[stripped_name] = (bone.name)
    
    # Save the bone dictionary to a file.
    try:
        with open(filepath, "w") as file:
            json.dump(bone_dict, file, indent =  4)
        print("Bone data saved to {0}.\n".format(filepath))
        
    except IOError as error:
        print("The following error was thrown trying to save bone data to the file {0}:\n{1}".format(filepath, error))


def strip_bone_names(regex, filepath):
    
    target_object = bpy.context.active_object
    armature = target_object.data
    
    for bone in armature.bones:
        #print("\n"+bone.name)
        
        # Match the regex, strip and update the bone name.
        match = re.search(regex, bone.name)
        if (match is not None):
            stripped_name = bone.name[0:match.span()[0]] + bone.name[match.span()[1]:] # concatenate so we can handle middle patterns.
            print("{0} will be stripped down to {1}.".format(bone.name, stripped_name))
            bone.name = stripped_name
        else:
            print("{0} does not include the pattern and will not be stripped.".format(bone.name))
    print("\nDone stripping bones.\n")
            

def save_strip_bone_names(regex, filepath):
    
    target_object = bpy.context.active_object
    armature = target_object.data
    
    bone_dict = {}
    bone_dict["regex"] = (regex)
    
    for bone in armature.bones:
        #print("\n"+bone.name)
        
        # Match the regex, add the bone and its stripped counterpart to the dictionary, then update the bone name.
        match = re.search(regex, bone.name)
        if (match is not None):
            stripped_name = bone.name[0:match.span()[0]] + bone.name[match.span()[1]:] # concatenate so we can handle middle patterns.
            print("{0} will be stripped down to {1}.".format(bone.name, stripped_name))
            bone_dict[stripped_name] = (bone.name)
            bone.name = stripped_name
        else:
            print("{0} does not include the pattern and will not be stripped.".format(bone.name))
    print("\nDone stripping bones.\n")
    
    # Save the bone dictionary to a file.
    try:
        with open(filepath, "w") as file:
            json.dump(bone_dict, file, indent =  4)
        print("Bone data saved to {0}.\n".format(filepath))
        
    except IOError as error:
        print("Error trying to save bone data to the file {0}:\n{1}".format(filepath, error))
        

def load_bone_names(regex, filepath):

    bone_dict = {}
    
    # Try to load the dictionary.
    try:
        with open(filepath, "r") as file:
            bone_dict = json.load(file)
            #print(bone_dict)
        
    except IOError as error:
        print("Error trying to read bone data from the file {0}:\n{1}".format(filepath, error))
        return
    
    # Make sure the regex used by this dictionary corresponds to the one we want to use.
    if (regex == bone_dict["regex"]):
        
        target_object = bpy.context.active_object
        armature = target_object.data
        
        for bone in armature.bones:
            if bone_dict.get(bone.name) is not None:
                print("{0} will be renamed to {1}.".format(bone.name, bone_dict[bone.name]))
                bone.name = bone_dict[bone.name]
            else:
                print("The bone {0} was not found in the source file and will not be renamed.".format(bone.name))
                
        print("\nDone renaming bones.\n")
        
    else:
        print("This bone data cannot be used as it was created using a different regex than the current one.")
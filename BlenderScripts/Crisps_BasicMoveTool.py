# Created by Roxane Morin, "Crisps" in the Sims 2 community.
# https://github.com/RoxaneMorin
# https://crispsandkerosene.tumblr.com/


bl_info = {
    "name": "Crisps's Basic Move Tool",
    "description": "Simple tool for moving objects to the scene's origin.",
    "author": "Roxane Morin (CrispsandKerosene)",
    "version": (0, 1),
    "blender": (2, 80, 0),
    "location": "View3D > Item",
    "category": "Object",
}


import bpy
from mathutils import Vector, Matrix


# GLOBAL PARAMETERS
valid_types = ["MESH", "CURVE", "FONT"]


## UI

# Make the move object button and its action.
class Crisps_move_operator(bpy.types.Operator):
    bl_idname = "object.move_to_scene_origin"
    bl_label = "Sit Me At Scene Origin" # default label.
    bl_description = "Moves the selected object(s) so the bottom of their bounding box touches the scene origin.\nTheir pivot(s)/origin(s) are moved but keep their object space positions.\nValid object types are Mesh, Curve, and Text/Font"

    @classmethod
    def poll(cls, context):
        # The button should only be active in Object Mode, if one or more objects of a valid type are selected.
        return bpy.context.object.mode == "OBJECT" and any(object.type in valid_types for object in context.selected_objects)
        
    def execute(self, context):
        sit_object_at_origin()
        return {'FINISHED'}
    
# 
class Crisps_move_origin_operator(bpy.types.Operator):
    bl_idname = "object.move_origin_to_scene_origin"
    bl_label = "Sit Me & My Origin At Scene Origin" # default label.
    bl_description = "Moves the selected object(s) and their pivot(s)/origin(s) so the bottom of their bounding box touches the scene origin"

    @classmethod
    def poll(cls, context):
        # The button should only be active in Object Mode, if one or more objects are selected.
        return bpy.context.object.mode == "OBJECT" and len(context.selected_objects) > 0
        
    def execute(self, context):
        sit_origin_at_origin()
        return {'FINISHED'}

# Make the panel.
class Crisps_move_panel(bpy.types.Panel):
    bl_idname = "OBJECT_PT_Crisps_move_panel"
    bl_label = "Crisps' Basic Move Tool"
    
    bl_space_type = "VIEW_3D"
    bl_region_type = "UI"
    bl_category = "Item"

    def draw(self, context):
        layout = self.layout
        
        # Move Object(s) to Scene Origin.
        # Change the button's label depending on the number of valid objects selected.
        valid_objects = [object for object in context.selected_objects if object.type in valid_types]
        no_of_valid_objects = len(valid_objects)
        if (no_of_valid_objects == 1):
            label_move = "Sit Me ({0}) At Scene Origin".format(valid_objects[0].name)
        elif (no_of_valid_objects > 1):
            label_move = "Sit Us At Scene Origin"
        else:
            label_move = "Select Valid Object(s) to Move"
        layout.operator("object.move_to_scene_origin", icon = "EMPTY_DATA", text = label_move)
        
        # Move Object(s) & their Origin(s) to Scene Origin.
        no_of_objects = len(context.selected_objects)
        if (no_of_objects == 1):
            label_origin = "Sit Me ({0}) & My Origin At Scene Origin".format(bpy.context.active_object.name)
        elif (no_of_objects > 1):
            label_origin = "Sit Us & Our Origins At Scene Origin"
        else:
            label_origin = "Select Object(s) to Move"
        layout.operator("object.move_origin_to_scene_origin", icon = "EMPTY_DATA", text = label_origin)


# Handle (un)registering.
def register():
    bpy.utils.register_class(Crisps_move_operator)
    bpy.utils.register_class(Crisps_move_origin_operator)
    bpy.utils.register_class(Crisps_move_panel)
    
def unregister():
    bpy.utils.unregister_class(Crisps_move_operator)
    bpy.utils.unregister_class(Crisps_move_origin_operator)
    bpy.utils.unregister_class(Crisps_move_panel)

# Main.
if __name__ == "__main__":
    register()


    
## FUNCTIONS

def sit_object_at_origin():
    
    original_active_object = bpy.context.view_layer.objects.active
    
    for selected_object in bpy.context.selected_objects:

        if (selected_object.type in valid_types):
            
            # Make the selected object active.
            bpy.context.view_layer.objects.active = selected_object 
            
            # Fetch the selected object's bounding box and world position matrix.
            bounding_box = selected_object.bound_box
            matrix_world = selected_object.matrix_world

            # Average the position of the bounding box's bottom points, found at indices 0, 3, 4, 7.
            averaged_location = Vector((0, 0, 0))
            for i in [0, 3, 4, 7]:
                vertex_world_coord = matrix_world @ Vector(bounding_box[i])
                averaged_location += vertex_world_coord
            averaged_location /= 4

            # Move it without altering the cursor or pivot point.
            bpy.ops.object.mode_set(mode = "EDIT")
            selected_object.data.transform(Matrix.Translation(averaged_location))
            selected_object.matrix_world.translation -= averaged_location
            bpy.ops.object.mode_set(mode = "OBJECT")

        else:
            print("Skipped an invalid or empty object.")
            #selected_object.select_set(False)
    
    bpy.context.view_layer.objects.active = original_active_object


def sit_origin_at_origin():
    
    scene_origin = Vector((0, 0, 0))
    original_active_object = bpy.context.view_layer.objects.active
    original_cursor_location = Vector(bpy.context.scene.cursor.location)
    
    for selected_object in bpy.context.selected_objects:
        
        # Make the selected object active.
        bpy.context.view_layer.objects.active = selected_object 
        
        # Fetch the selected object's bounding box and world position matrix.
        bounding_box = selected_object.bound_box
        matrix_world = selected_object.matrix_world

        # Average the position of the bounding box's bottom points, found at indices 0, 3, 4, 7.
        averaged_location = Vector((0, 0, 0))
        for i in [0, 3, 4, 7]:
            vertex_world_coord = matrix_world @ Vector(bounding_box[i])
            averaged_location += vertex_world_coord
        averaged_location /= 4
        
        # Reposition the cursor at that location, then move the object's origin.
        bpy.context.scene.cursor.location = averaged_location
        bpy.ops.object.origin_set(type = "ORIGIN_CURSOR")
        
        # Move the object to the scene's origin.
        selected_object.location = scene_origin
        
        # Reposition the cursor at the scene's origin and redo object origin to cursor.
        bpy.context.scene.cursor.location = scene_origin
        bpy.ops.object.origin_set(type = "ORIGIN_CURSOR")

    # Restore original state.
    bpy.context.view_layer.objects.active = original_active_object
    bpy.context.scene.cursor.location = original_cursor_location
    
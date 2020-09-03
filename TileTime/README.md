# TileTime! - code exercises from HBS

## rectangle_problem
Scene where given a point in 2d space, determine the closest point on a rect and the distance to 2d point. 
returns 0 if that point is inside the rect.

![Alt Text](https://github.com/deserializeme/Game-Projects/blob/main/media/gifs/tiletime.gif)

### Features
- Visual debugging! Test a point by left clicking
- Enable the "every frame" option to test the mouse-position in real-time
- Disable the "draw gizmos" option to test w/out visuals for performance tuning
- in editor you can change the scale and location of the rect for more thorough testing.

### known limitations
- only supports 4-sided shapes
- can't change individual points of the rect since its a hard-coded rect based on the min/max x/y values (Height = Length/2)
- side detection breaks if rect has a negative scale, so the value is a range starting at 1, you dont need a negative scale rect, so im not worried about it
- intersection point detection precision could be higher, but seems unecessary for this use case.

scripts:
 click_for_point.cs - handles getting your mouse position
 find_intersection.cs - math library of static functions to handle to heavy vector math work.
 rect_maker.cs - struct that defines the rect we will be testing with as defined in requirements, added a couple fields because i make my own destiny.
 rect_manager.cs - manages the rect, mouse position, and most of the core loop. serves as our link to other scripts (namely the UI)
 ui_manager.cs - pulls values from rect_manager.cs to drive a small UI.











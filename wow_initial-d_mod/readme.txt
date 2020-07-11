in action: https://www.youtube.com/watch?v=ymEKgkLbwTM

1. download addon "WeakAurasStopMotion"
2. place sheet.tga into directory World of Warcraft\Interface\AddOns\WeakAurasStopMotion\Textures\Basic
3. open Warcraft\Interface\AddOns\WeakAurasStopMotion\StopMotionTextures.lua in a text editor (notepad++) preferred or if that makes you nervous i have included an edited version in the folder you can just replace yours with. just make sure that the .tga inmage file's name doesnt get changed.

insert the folowing as line 21:

["Interface\\AddOns\\WeakAurasStopMotion\\Textures\\Basic\\sheet"] = "Speedy",

insert the following as lines 64-68:

WeakAurasStopMotion.texture_data["Interface\\AddOns\\WeakAurasStopMotion\\Textures\\Basic\\sheet"] = {
     ["count"] = 64,
     ["rows"] = 8,
     ["columns"] = 8
  }


4. place audio files in the World of Warcraft\Interface\AddOns\WeakAuras\PowerAurasMedia\Sounds directory



you should now be able to create a "stop motion" type WA from the /wa menu

5. set the trigger to whatever speedboost spell you want to use, and under actions choose "play sound" on show

6. choose "custom" for the sound and then paste in the directory file path to the sound file 
example: Interface\AddOns\WeakAuras\PowerAurasMedia\Sounds\90s.ogg

7. under the actions tab chose "stop sound" under the on hide menu



make sure you restart wow if you had it open while making these edits, otherwise if wont work.
Sorry for the shitty instructions but its 3:30 am and im tired AF 







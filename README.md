# graph-viewer-vr
This Unity App plots graphml files into 3D space to be displayed on Android devices 
in virtual reality.

# How to use the App
Just import this project in Unity (2019.3.0 or higher) including Standard Assets. To 
view your own files insert your graphml files into the Assets/Resources directory and
change the public variable inputFile in GraphDrawer class manually. 

# How to get graphml files
Use gephi version 8.2 with the Atlas 3D plugin to generate the graphml files. We make 
use of gephi because it makes the math for us. 
Important: You need to change the file extension to .xml because the Unity TextAsset 
is not compatible with graphml files.

# How to change the logo
Add your logo to the Assets/Standard Assets/2D/Sprites/ directory. Select your logo 
in Unity. In the Inspector select "Sprite (2D and UI)" as Texture Type. 
Select the LogoHolder->Logo in the Hirarchy and change the Source Image in the Inspector.

# Screenshots
![screenshot](/Images/screenshot_01.png?raw=true "screenshot 1")

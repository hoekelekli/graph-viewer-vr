# General Information
This tool only parses graphml-files and plots them in Unity. Thus we make use of the tool Gephi. 
In a nutshell, the architecture is as follows: 
![screenshot](/Images/architecture.png?raw=true "Architecture")

# Setup
For this purpose we use Gephi (v0.8.2 beta) and install the plugin Force Atlas 3D. Newer versions of 
Gephi do not support the plugin. 
Also, install Unity including the Standard Assets and the Android SDK. 

# Exporting GraphMl files with Gephi
First, create a adjacency matrix. Save this as a csv-file and use semicolon as seperator. You can use
the given example in Assets/Resources/gephi_input.csv.
Open the file in Gephi and choose Force Atlas 3D as Layout on the left panel. Check the 3D checkbox
and run the tool.

![screenshot](/Images/gephi_1.PNG?raw=true "Gephi Screenshot 1")

It takes a while and may not terminate. Just abort the algorithm after a while. 
Go to Files -> Export -> Graph file and select GraphML files. Go to options and check the Normalize 
checkbox. Save the GraphMl file in the Assets/Resources directory and rename it to <filename>.xml instead of 
<filename>.graphml. Also manually check if Gephi has exported all 3 axis. Sometimes it exports only 2 axis, for some reason.
If you are confronting such a problem, just restart Gephi and try exporting the graph again.

![screenshot](/Images/gephi_2.PNG?raw=true "Gephi Screenshot 2")

# Importing and running GraphMl files in Unity
After you have imported the project in Unity select GraphViewer in the Hierarchy. Then change the 
variable "Input File" in the Inspector at the right side. Only fill in the file name without the 
extension. 

![screenshot](/Images/unity_1.PNG?raw=true "Unity Screenshot 1")

Go to File -> Build Settings and switch to Android if it is not already selected. Then you are 
ready to build the application and run it on your Android device. If you setup a debugging environment
you can also click "build and run" which directly runs the application on your connected device. 

![screenshot](/Images/unity_2.PNG?raw=true "Unity Screenshot 2")

If the graph is displayed not clearly, you might change the CoordinateScale or LineScale in the 
GraphDrawer. Take a look to the doxygen documentation for further information.

![screenshot](/Images/unity_3.PNG?raw=true "Unity Screenshot 3")

# How to change the logo
Add your logo to the Assets/Standard Assets/2D/Sprites/ directory. Select your logo 
in Unity. In the Inspector select "Sprite (2D and UI)" as Texture Type. 
Select the LogoHolder->Logo in the Hirarchy and change the Source Image in the Inspector.

![screenshot](/Images/unity_4.PNG?raw=true "Unity Screenshot 4")

# Unity Project Settings
To view your project settings go to "Edit -> Project Settings ...". You can configure different settings such as 
input management or player settings. 
For using the application on android in virtual reality it is neccessary to check the "Virtual Reality Supported"
checkbox under "Player -> XR Settings".

![screenshot](/Images/unity_5.PNG?raw=true "Unity Screenshot 5")

# Useful References
Keypad of our used controller: https://forum.unity.com/threads/utopia-360-bluetooth-controller-mapping-buttons-to-unity.482773/
Unity Input Settings: https://docs.unity3d.com/Manual/ConventionalGameInput.html
Android Debugging: https://bertt.wordpress.com/2018/06/12/how-to-debug-your-unity3d-android-application-in-visual-studio/






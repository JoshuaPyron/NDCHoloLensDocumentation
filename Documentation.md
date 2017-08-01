# NDC Holographic Assistant
Welcome to the NDC HoloLens Documentation! \
Click [here](User_Help.md "User Manual") if you are looking for help with using the NDC Holographic Assistant.\
It is recommended that you have a basic understanding of coding in Unity before looking at this documentation.

1. [DOTween (animations)](#dotween)
2. [Adding Products](#adding-products)
     1. [Model Requirements](#model-requirements)
     2. [Physical Product](#physical-product)
     3. [Physical Part](#physical-part)
     4. [Service Scripts](#service-scripts)
     5. [Adding a Product to the List](#adding-a-product-to-the-list)
3. [Designing a Menu](#designing-a-menu)
4. [Online Connections](#online-connections)
    1. [Connecting to a Server](#connecting-to-a-server)
    2. [Connecting to a Session](#connecting-to-a-session)
	  3. [Adding Custom Messages](#adding-custom-messages)
		    1. [Sending Messages](#sending-messages)
		    2. [Receiving Messages and Processing](#receiving-messages-and-processing)
5. [Recommended Links](#recommended-links)

## DOTween
  DOTween is a free add-on for Unity. It adds multiple methods and Classes designed to assist in creating fluid animations in scripts. The most Prominent of which is the Tween class.
  There are various Methods that can chain together to make custom animations in your script. For example, the following would move myObject to the point 1,2,3 in half a second after waiting 0.1 seconds using the InOutCubic Ease function:
  ```csharp
  myObject.transform.DOLocalMove(new Vector3(1,2,3),0.5f).SetDelay(0.1f).SetEase(Ease.InOutCubic);
  ```

  _*ADDITIONS NEEDED!!!!!!*_
#### [Easings](#http://easings.net/ "Ease Functions")
  Easings are functions that smooth the movement of objects. For example, InOutCubic gives a slow beginning and end, while the middle goes faster. \
  Click on the section title to see a list of Ease functions.

## Adding Products
#### Model Requirements
  * The Product will be assembled in Unity
  * Each part must be its own model
  * It is recommended that the product's prefab be structured using various empty game objects to sort the parts
  * See the configured Prefabs in the Assets/"Models(3d)"
  * The models must be in one of the following formats:
    * .FBX
    * .DAE (Collada)
    * .3DS
    * .DXF
    * .OBJ
    * .SKP
#### Physical Product
  When the parts are all in place, drag PhysicalProduct.cs into the parent game object (the container of all of the parts).\
  In the inspector, there should be 3 different sections, Service Animation, Demo Animation, and Disable When Minimized.
###### Service Animation
  If you wish to add animations to a product when it is selected for the service menu, add the animations here. For example, the front plate of the Accuscan is removed and placed into the back.
###### Demo Animation
  If you wish to develop an animation that the product will play when demoed, place it here, for example the Ultrascan plays a small animation of it disassembling and reassembling.
###### Disable When Minimized
  In this section, add all of the parts not visible on the outside. The parts added to this section are disabled unless the product is selected to be serviced. This increases performance.
###### Remaining Values
  Provide a float that represents the scale of a product when in the selection screen. This will allow the program to scale the product appropriately based on its size.\
  Provide 3 floats that represent the rotation of a product so that it is angled properly in the selection screen.
#### Physical Part
  Each part and containers of parts must have a Physical Part script in order to appear in the Service menu. Her you must fill out the name, instructions on how to remove the part (assuming the other necessary parts are removed), animations, and the parts that need to be removed before this part.

  The Physical Product has a list of parts which contains its children that contain a PhysicalPart script. If Part A has a child that is a part (Part B), then A has a list of parts that contains part B. This results in a Part Hierarchy. For example, In the Accuscan, PhysicalProduct.parts = { Metal Housing, Circuit Board, Brackets, Laser System, Shell Plate }; however, Brackets has children that are parts so PhysicalProduct.parts[2].parts = { BracketTR, BracketTL, ... }.
###### Editor Tools
  The "Actual Position Offset" is where the pointer will point to and "Product Rotation To Make Visible" is the rotation where the part is best seen. These values can be changed form the inspector, or if you check "Enable Editor Tools", you can move these points inside of the scene.
###### Animation
  Drag TransitionInPPAnimationModule.cs, This give you some options to remove this part quickly, inside of the service menu before selecting a part to service.
###### Service Part Animation
  In the Remove Steps array, add all of the parts that must be removed before this part, in the order they must be removed. For example, the circuit board on the Accuscan has Bolts at index 0 and the plate at index 1.\
  In Populate Step, place the animation to be played to remove the part. To do this, click the plus and drag and drop the game object that has the service script into the new event. Then select the animation method from the list (This will be covered further in the [Service Scripts](#service-scripts) section). If you do not wish to develop your own animations for a part, PhysicalProductAnimator has some default basic animations you can choose from.\
  In Servicing Tools, add to the array and select the tools needed for the job.\
  In Servicing Materials, add to the array and add the needed materials to replace the part.\
#### Service Scripts
  The Service Scripts are used to create animations for each of the parts when they are removed during servicing. To start, create a script (Usually of the format: "ProductNameService") that inherits PhysicalProductAnimator. The Start method is where you designate the products layers (much like an onion has layers, if you want to see part A but it is be hind Part B, create an animation to remove Part B and Show Part A) and should initially look something like the following:
  ```csharp
  protected override void Start() {
          base.Start();
          physicalProduct.justMovedToThisLayer +=delegate (PhysicalSceneObject THIS, bool down) {
              if (down) { resetProduct(); }//THIS LINE MUST EXIST. It helps reset the product on a few occasions
              else {  }
    }  }
  ```
  When a part is in a new layer, be sure to add an animation for that part formatted like the following:
  ```csharp
  physicalProduct.parts[0]+=delegate (PhysicalSceneObject THIS, bool down) {
      if (down) {  }
      else {  }
  }
  ```
  When down is true, that means that the transition is coming downward from the layer above. For example, the Laserscan's Laser system is below the shell, so moving from the shell to the Laser system would play the animation in the physicalProduct.parts[0] down section of the if statement. PhysicalProduct.justMovedToThisLayer is the highest layer and is dedicated to resetting the product to its original positions.


  Any custom animations for removing specific parts should be placed in its own method in this file. When setting up PhysicalPart, simply select the animation for that part.

  See [UltrascanService.cs](Examples/UltrascanService.cs) for Reference.
#### Adding a Product to the List
  After a Product has been configured and all of the scripts have bee added, save the game object as a prefab by dragging the game object to the project window. Then, in the Assets/Resources folder select the DataStore Object. Press the Plus in the Products drop down, provide the requested information, drag in the new prefab, and select whether or not it can be serviced or demoed.

  \*Note that this is also the area you can add Tools that the technician can use.

## Designing a Menu
  In Unity designing a menu for the HoloLens is fairly straightforward and requires little additional effort compared to the Standard Unity UIs; however, there are a few things that should be known. \

  You should avoid putting a canvas inside of another canvas.\
  You can use the Prefabs in the Assets/Prefab/UI folder to keep with the theme, style, and interactions we have developed so far.\
  Remember that the UI should be in the World Space, Not Screen Space. If placed in the screen space, the user will not be able to interact with any of the objects because the HoloLens uses the Gaze direction and the UI would move with the screen.

## Online Connections
  Being able to share Holograms, whether in the same room or across the country, is a very interesting part of the HoloLens.
  This section will concentrate on connecting to a server, creating, sending, and receiving messages.

  The general concept of connecting HoloLenses relies on sending and receiving  messages. In the HoloLens, whenever a button is pressed or an object moved, the system sends a message to the server, which is then distributed to all of the other HoloLenses. When the message is received, each system used the information in the message to alter its own environment.  For example, if you pressed a button, the HoloLens would send a message to tell the others about the button being pressed, which would then be processed by the HoloLens pressing the same button.

  Messages are limited to using very simple information, like strings, bytes, ints, floats, etc; however, you can use many lines of information. For example, to send a Vector3, you could send the x, y, and z components as separate lines of the same message, or use the helper method ```AppendVector3(Vector)```.

  Firstly, drag the Networking Prefab into the Scene, or create an empty game object and drag in the Sharing Stage and Network Controller scripts.
#### Connecting to a Server
  Connecting to a server is relatively simple. Simply drag in the provided Networking Prefab, or drag the Sharing Stage script onto an empty game object. In this, you should fill in the Server Address and Server Port areas in the inspector. If you want to set these values in your code, use the following inside of NetworkController.cs:
  ```csharp
  stage.Manager.SetServerConnectionInfo(/*server address*/,(byte)/*server port as int*/);
  ```
#### Connecting to a Session
  Sessions are like the server's rooms. Each session holds and communicates with only those inside of it. This way, you can have multiple groups in their own sessions on the server without getting the interactions jumbled.
  Connecting to a session is as easy as inputting the following line of code
  ```csharp
  NetworkController.main.Join(/*session object*/);
  //or
  NetworkController.main.FindandJoin(/*session name*/);
  //or
  NetworkController.main.FindandJoin();//uses the provided Default Session name
  ```
  These methods return true if they successfully connect to the session.

  Though the NDC Holographic Assistant does not allow users to create sessions, this is possible by using
   ```csharp
  NetworkController.main.CreateSession(/*session name*/);
  ```
#### Adding Custom Messages
  Messages are sent in-between devices to communicate actions or provide data. Making out messages is how you tell the other systems what you just did in the environment.\
  Creating a message is done inside of the NetworkController.cs. First, add the new Message type to the SharedMessageType Enum. The method should look like the following:
  ```csharp
  public void MethodName(/*any data you wish to enter into your message*/){
    if(canSend){
      NetworkOutMessage nm = CreateMessage(SharedMessageType.Name);
      nm.Write(Data);
      nm.Write(Data);
      ...
      this.serverConnection.Broadcast(nm, MessagePriority.Immediate, MessageReliability.ReliableSequenced, MessageChannel.Avatar);
    }
  }
  ```
  Then inside of the OnMessageRecieved method, add the new Enum to a case in the switch statement. The case will execute when a message of this type is recieved.\
  There are some helper methods to clean up you code, like AppendVector3, ReadVector3, AppendTransform, etc.\
  You can change the Enums in the ``this.serverConnection...`` statement in order to ensure that the other devices catch and process then message, or send the message more frequently.


###### Sending Messages

###### Receiving Messages and Processing

## Recommended Links

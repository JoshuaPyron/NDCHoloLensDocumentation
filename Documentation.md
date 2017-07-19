# NDC Holographic Assistant
Welcome to the NDC HoloLens Documentation! \
Click [here](User_Help.md) if you are looking for help with using the NDC Holographic Assistant.\
It is recommended that you have a basic understanding of coding in Unity before looking at this documentation.

1. [DOTween (animations)](#dotween)
2. [Adding Products](#adding-products)
     1. [Model Requirements](#model-requirements)
     2. [Service Scripts](#service-scripts)
     3. [Physical Product](#physical-product)
     4. [Physical Part](#physical-part)
     5. [Adding a Product to the List](#adding-a-product-to-the-list)
3. [Online Connections](#online-connections)
	1. [Connecting to a Server](#connecting-to-a-server)
  2. [Connecting to a Session](#connecting-to-a-session)
	2. [NetworkOutMessage and NetworkInMessage](#networkoutmessage-and-networkinmessage)
	3. [Adding Custom Messages](#adding-custom-messages)
		1. [Sending Messages](#sending-messages)
		2. [Receiving Messages and Processing](#receiving-messages-and-processing)

***
## DOTween
DOTween is a free add-on for Unity. It adds multiple methods and Classes designed to assist in creating fluid animations in scripts.


## Adding Products

#### Model Requirements

#### Service Scripts

#### Physical Product

#### Physical Part

#### Adding a Product to the List


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

#### NetworkOutMessage and NetworkInMessage

#### Adding Custom Messages

###### Sending Messages

###### Receiving Messages and Processing

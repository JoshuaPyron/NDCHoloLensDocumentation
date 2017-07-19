# NDC HoloLens Experience
Welcome to the NDC HoloLens Documentation! \
Click [here](User_Help.md) if you are looking for help with using the HoloLens program.\
It is recommended that you have a basic understanding of coding in Unity before looking at this documentation.

1. [DOTween (animations)](#Dotween)
2. [Adding Products](#Adding-Products)
     1. [Model Requirements](#Model-Requirements)
     2. [Service Scripts](#Service-Scripts)
     3. [Physical Product](#Physical-Product)
     4. [Physical Part](#Physical-Part)
     5. [Adding a Product to the List](#Adding-a-Product-to-the-List)
3. [Online Connections](#Online-Connections)
	1. [Connecting to a server](#Connecting-to-a-Server)
	2. [NetworkOutMessage and NetworkInMessage](#NetworkOutMessage-and-NetworkInMessage)
	3. [Adding Custom Messages](#Adding-Custom-Messages)
		1. [Sending Messages](#Sending Messages)
		2. [Receiving Messages and Processing](#Receiving-Messages-and-Processing)

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

The general concept of connecting HoloLenses relies on sending and receiving  messages. These are used to communicate actions across the internet. A good way of imagining the interaction is having 2 panels of buttons. When you hit a button, you tell the other person that you hit the button. The person then hits the same button in order to remain at the same point as you. In the HoloLens, whenever a button is pressed or an object moved, the system sends a message to the server, which is then distributed to all of the other HoloLenses. When the message is received, each system used the information in the message to alter its own environment.

Messages are used to tell the other HoloLenses what your HoloLens just did. For example, if you pressed a button, the HoloLens would send a message to tell the others about the button being pressed.
Messages are limited to using very simple information, like strings, bytes, ints, floats, etc. You are limited to these data types; however, you can use many lines of information. For example, to send a Vector3, you could send the x, y, and z components as separate lines of the same message.

#### Connecting to a Server
Connecting to a server is relatively simple. Simply drag in the provided Networking Prefab, or drag the Sharing Stage script onto an empty game object. In this, you should fill in the Server Address and Server Port areas in the inspector. If you wish to automatically join the server,

#### NetworkOutMessage and NetworkInMessage

#### Adding Custom Messages

###### Sending Messages

###### Receiving Messages and Processing

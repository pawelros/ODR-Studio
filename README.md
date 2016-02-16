# ODR-Studio

ODR-Studio is a very basic (yet) concept of free, scallable, cross platform, responsive DAB+ studio web application. The idea is quite simple - to create a web app, that together with Open Digital Radio great tools will bring you a real radio studio experience and all of that directly from your browser.

### Features
* Can run on Linux, Windows, OS X
* UI works fine on mobile devices
* Displays info about currently played track
* Displays currently broadcasted MOT Slideshow
* Displays currently broadcasted  DLS
 
### How it looks like

#### In full HD
![alt text](https://raw.githubusercontent.com/Rosiv/ODR-Studio/cleanup/doc/web_gui_full_hd.png "Full HD Web GUI")

#### On a mobile
![alt text](https://raw.githubusercontent.com/Rosiv/ODR-Studio/cleanup/doc/web_gui_mobile.png "Mobile Web GUI")

### What's next
The list of desired features is huge, the next goals are to implement:
* Play, Stop, Pause buttons
* Playlist
* Manual MOT Slideshow image upload
* MongoDB provider for app settings, DLS and MOT Slideshow images
* Music database management
* Step by step further VLC functionality
* Standalone web stream proxy in order to never turn down fdk-aac-dabplus encoder
* Support multiple audio sources and mix them in realtime, into web stream proxy (JACK maybe?)
* And many, many more
 
### Installation

ODR-Studio repo contains two projects: OdrStudio.WebApi and OdrStudio.WebApp.
Both are build on shiny, bright, tremendous [.NET Core](http://docs.asp.net/en/latest/conceptual-overview/dotnetcore.html) that has already begun changing the future of responsive web app development. In order to run both application the only thing you need is to install [DNX](https://github.com/dotnet/cli) on your system. You can install it on any supported platform, but since the recomended OS for running Open Digital Radio tools is debian, here is the long story short how to bootstrap it on debian:

Debian 8 (Jessie) - not supported due to liblldb-3.6 dependency (cons of using the frameworks that are in RC stage)
Debian 9 (Stretch)

clone the repo

git clone git@github.com:Rosiv/ODR-Studio.git

cd 



edit

src/OdrStudio.WebApi/appsettings.json to match your multiplekser and VLC configuration:

```javascript
  "Player":{
      "Uri" : "http://192.168.1.2:8080",
      "Username" : "",
      "Password" : "test",
      "MotSlideshowUri" : "file:///mnt/hgfs/artistalbum",
      "MotFifo" : "/home/pr/odr_studio/data/mot_slideshow"
  }
}
```

Uri - address of your VLC web stream
Username - username with access to your VLC web API
Password - VLC user password
MotSlideshowUri - filesystem or network path to the directory where VLC keeps album cover images. In my case VLC was installed on a Windows machine and I have just mounted C:\Users\pr\AppData\roaming\VLC\artistalbum to file:///mnt/hgfs/artistalbum on my debian.
MotFifo - since mot-encoder reads only from fifo, we need a one.

### Usage

1. Start http streaming on your VLC
2. Make sure that VLC API is working and you have access to it
3. Configure your multiplexer, (at least fdk-aac-dabplus, mot-encoder, ODR-DabMux)
4. Run ODR-Studio Web API and Web GUI
5. Navigate to http://localhost:5000




![alt text](https://raw.githubusercontent.com/Rosiv/ODR-Studio/cleanup/doc/Workflow.jpg "Workflow")

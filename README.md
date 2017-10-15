# ODR-Studio
This is just a test2

ODR-Studio is a very basic (yet) concept of free, scallable, cross platform, responsive DAB+ studio web application. The idea is quite simple - to create a web app, that together with [Open Digital Radio](https://github.com/Opendigitalradio) great tools will bring you a real radio studio experience and all of that directly from your browser.

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
Both are build on shiny, bright, tremendous [.NET Core](http://docs.asp.net/en/latest/conceptual-overview/dotnetcore.html) that has already begun changing the future of responsive web app development. In order to run both application the only thing you need is to install [.NET Core](http://docs.asp.net/en/latest/getting-started/index.html) on your system. You can install it on any supported platform, but since the recomended OS for running Open Digital Radio tools is debian, here is the long story short how to bootstrap it on debian:


#### 1. Install unzip and curl if you don’t already have them:

 ```sudo apt-get install unzip curl```

#### 2. Download and install DNVM:

```curl -sSL https://raw.githubusercontent.com/aspnet/Home/dev/dnvminstall.sh | DNX_BRANCH=dev sh && source ~/.dnx/dnvm/dnvm.sh```

Once this step is complete you should be able to run dnvm and see some help text.

#### 3. Install the .NET Execution Environment (DNX)

The .NET Execution Environment (DNX) is used to build and run .NET projects. Use DNVM to install DNX for Mono or .NET Core (see Choosing the Right .NET For You on the Server).

To install DNX for .NET Core:

Install the DNX prerequisites:

```
sudo apt-get install libunwind8 gettext libssl-dev libcurl4-openssl-dev zlib1g libicu-dev uuid-dev
```

Use DNVM to install DNX for .NET Core:

```
dnvm upgrade -r coreclr
```

#### 4. Install libuv

Libuv is a multi-platform asynchronous IO library that is used by Kestrel, a cross-platform HTTP server for hosting ASP.NET 5 web applications.

To build libuv you should do the following:

```
sudo apt-get install make automake libtool curl
curl -sSL https://github.com/libuv/libuv/archive/v1.8.0.tar.gz | sudo tar zxfv - -C /usr/local/src
cd /usr/local/src/libuv-1.8.0
sudo sh autogen.sh
sudo ./configure
sudo make
sudo make install
sudo rm -rf /usr/local/src/libuv-1.8.0 && cd ~/
sudo ldconfig
```


#### 5. Now you are ready to run the apps:

Clone the repo

```
git clone git@github.com:Rosiv/ODR-Studio.git

cd ODR-Studio/src/OdrStudio.WebApi/

#restore all dependencies
dnu restore

#run the app
dnx web

#follow the same steps for OdrStudio.WebApp
```

edit src/OdrStudio.WebApi/appsettings.json to match your multiplex and VLC configuration:

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
4. Run ODR-Studio Web API and Web GUI by executing dnx web command
5. Navigate to http://localhost:5000 (Web GUI) and http://localhost:5001/player/status (Web API)

### Workflow

Here is a workflow diagram to illustrate the whole architecture: 


![alt text](https://raw.githubusercontent.com/Rosiv/ODR-Studio/cleanup/doc/Workflow.jpg "Workflow")

Feel free to poke me if you have any questions.
Cheers

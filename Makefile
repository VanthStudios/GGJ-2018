# This Makefile is thinked to run over Travis CI so the following variables
# must be guarantee:
# - TRAVIS_REPO_SLUG

BASE_URL= http://netstorage.unity3d.com/unity
HASH= a9f86dcd79df
VERSION= 2017.3.0f3

PACKAGE= \
	"MacEditorInstaller/Unity-$(VERSION).pkg" \
	"MacEditorTargetInstaller/UnitySetup-Windows-Support-for-Editor-$(VERSION).pkg" \
	"MacEditorTargetInstaller/UnitySetup-Linux-Support-for-Editor-$(VERSION).pkg"

LOG_PATH= "`pwd`/unity.log"
PROJECT= `echo $(TRAVIS_REPO_SLUG) | cut -d '/' -f 2`

all: windows linux osx  
	echo "All builds were built successfully."

install:
	for i in $(PACKAGE); do \
		url="$(BASE_URL)/$(HASH)/$$i"; \
		basePackage=`basename $$url`; \
		echo "Downloading from $$url: "; \
		curl -o $$basePackage $$url; \
		echo "Installing $$basePackage"; \
		sudo installer -dumplog -package $$basePackage -target /; \
	done;

osx:
	echo "Attempting to build $(PROJECT) for OS X Universal"; \
	/Applications/Unity/Unity.app/Contents/MacOS/Unity \
		-batchmode \
		-nographics \
		-silent-crashes \
		-logFile $(LOG_PATH) \
		-projectPath `pwd` \
		-buildOSXUniversalPlayer "`pwd`/Build/osx/$(PROJECT).app" \
		-quit
	if [ $$? -ne 0 ]; then \
		echo "OS X Universal: exit during the build with code $$?"; \
		cat $(LOG_PATH); \
		exit 1; \
	fi; \
	echo "OS X Universal: done!";

linux:
	echo "Attempting to build $(PROJECT) for Linux Universal"
	/Applications/Unity/Unity.app/Contents/MacOS/Unity \
		-batchmode \
		-nographics \
		-silent-crashes \
		-logFile $(LOG_PATH) \
		-projectPath `pwd` \
		-buildLinuxUniversalPlayer "`pwd`/Build/linux/$(PROJECT)" \
		-quit
	if [ $$? -ne 0 ]; then \
		echo "Linux Universal: exit during the build with code $$?"; \
		cat $(LOG_PATH); \
		exit 1; \
	fi; \
	echo "Linux Universal: done!";

windows:
	echo "Attempting to build $(PROJECT) for Windows"
	/Applications/Unity/Unity.app/Contents/MacOS/Unity \
		-batchmode \
		-nographics \
		-silent-crashes \
		-logFile $(LOG_PATH) \
		-projectPath `pwd` \
		-buildWindowsPlayer "`pwd`/Build/windows/$(PROJECT).exe" \
		-quit
	if [ $$? -ne 0 ]; then \
		echo "Windows: exit during the build with code $$?"; \
		cat $(LOG_PATH); \
		exit 1; \
	fi; \
	echo "Windows: done!";

zip:
	echo "Attempting to zip builds"
	zip -r linux_u.zip `pwd`/Build/linux/
	zip -r osx_u.zip `pwd`/Build/osx/
	zip -r windows.zip `pwd`/Build/windows/
	echo "All zip are ready for the deploy!"












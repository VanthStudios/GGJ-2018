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
ZIP_PATH= "ggj_2018"

all: windows
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

windows:
	echo "Attempting to build $(PROJECT) for Windows"
	/Applications/Unity/Unity.app/Contents/MacOS/Unity \
		-batchmode \
		-nographics \
		-silent-crashes \
		-logFile $(LOG_PATH) \
		-projectPath `pwd` \
		-buildWindowsPlayer "../$(ZIP_PATH)/release/$(PROJECT).exe" \
		-quit
	if [ $$? -ne 0 ]; then \
		echo "Windows: exit during the build with code $$?"; \
		cat $(LOG_PATH); \
		exit 1; \
	fi; \
	echo "Windows: done!";

zip:
	echo "Attempting to zip the build"
	cp -r . "../$(ZIP_PATH)/source"
	mv "../$(ZIP_PATH)/source/LICENSE" "../$(ZIP_PATH)"
  zip -r $(ZIP_PATH).zip $(ZIP_PATH)
	echo "The zip is ready for the deploy!"

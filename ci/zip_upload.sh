#!/usr/bin/env bash

set -e
set -x

echo "Zipping archives"

ls -la ./
apt-get update
apt-get install -y zip unzip
ARCHIVE_NAME=01-Deploy-$(date +%F_%H-%M-%S)
zip -q -r ./$ARCHIVE_NAME.zip ./Builds
md5=`md5sum $ARCHIVE_NAME.zip | awk '{ print $1 }'`
echo "${md5}"
ls -la ./

# export BUILD_PATH=./Builds/$BUILD_TARGET/
# mkdir -p $BUILD_PATH

# ${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' /opt/Unity/Editor/Unity} \
#   -projectPath $(pwd) \
#   -quit \
#   -batchmode \
#   -buildTarget $BUILD_TARGET \
#   -customBuildTarget $BUILD_TARGET \
#   -customBuildName $BUILD_NAME \
#   -customBuildPath $BUILD_PATH \
#   -customBuildOptions AcceptExternalModificationsToPlayer \
#   -executeMethod BuildCommand.PerformBuild \
#   -logFile

# UNITY_EXIT_CODE=$?

# if [ $UNITY_EXIT_CODE -eq 0 ]; then
#   echo "Run succeeded, no failures occurred";
# elif [ $UNITY_EXIT_CODE -eq 2 ]; then
#   echo "Run succeeded, some tests failed";
# elif [ $UNITY_EXIT_CODE -eq 3 ]; then
#   echo "Run failure (other failure)";
# else
#   echo "Unexpected exit code $UNITY_EXIT_CODE";
# fi

# ls -la $BUILD_PATH
# [ -n "$(ls -A $BUILD_PATH)" ] # fail job if build folder is empty
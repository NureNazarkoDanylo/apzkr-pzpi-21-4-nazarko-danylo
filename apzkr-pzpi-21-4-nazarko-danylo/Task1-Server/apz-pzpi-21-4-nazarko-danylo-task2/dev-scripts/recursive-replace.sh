#!/usr/bin/env sh

PATH_TO_ROOT_DIRECTORY="${1}"
STRING_TO_BE_REPLACED="${2}"
STRING_TO_REPLACE_TO="${3}"

find "${PATH_TO_ROOT_DIRECTORY}" -type f -print -exec perl -0777 -i -pe "s/${STRING_TO_BE_REPLACED}/${STRING_TO_REPLACE_TO}/g" {} \;

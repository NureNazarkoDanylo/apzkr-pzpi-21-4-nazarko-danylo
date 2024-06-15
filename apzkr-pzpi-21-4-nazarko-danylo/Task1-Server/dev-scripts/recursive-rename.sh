#!/usr/bin/env sh

PATH_TO_ROOT_DIRECTORY="${1}"
NAME_TO_BE_REPLACED="${2}"
NAME_TO_REPLACE_TO="${3}"

files=$(find "${PATH_TO_ROOT_DIRECTORY}" -name "*${NAME_TO_BE_REPLACED}*")

for relative_file_path in ${files}; do

    number_of_fields=$(printf '%s\n' "${relative_file_path}" | awk --field-separator '/' '{ print NF - 1}')
    relative_folder_path=$(printf '%s\n' "${relative_file_path}" | cut -d '/' -f "-${number_of_fields}")
    file_name=$(printf '%s\n' "${relative_file_path}" | awk --field-separator '/' '{ print $NF }')
    new_name=$(printf '%s\n' "${file_name}" | sed "s/${NAME_TO_BE_REPLACED}/${NAME_TO_REPLACE_TO}/")

    new_file_path="${relative_folder_path}/${new_name}"
    mv "${relative_file_path}" "${new_file_path}"

    printf "Moved\n  '%s'\nto\n  '%s'\n\n" "${relative_file_path}" "${new_file_path}" 
done

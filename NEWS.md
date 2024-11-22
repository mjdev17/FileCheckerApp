# News and updates

## [0.2.0] - 2024-11-21

Migration and cross-platform support.

This version has been migrated to the Avalonia UI framework, which involved modifying and adapting certain features to ensure compatibility. Despite these changes, the application remains stable and retains the functionality of version 0.1.0.

List of changes:

- Full migration of the project to the Avalonia UI framework, adapting the existing interfaces and features to ensure compatibility and stability.
- Support for operating system themes, including light and dark modes.
- New tool for comparing the content of two files, supporting common text formats such as .txt, .json, .xml, .html, .css, .js, among others.
- Versions available for Linux and macOS. Please note that compatibility with Linux distributions has not been fully tested, and performance on macOS may vary depending on the processor (especially on Intel vs. M1/M2 chips).

>[!WARNING]  
> Responsible use of the new tool is recommended, as it is still in an early implementation phase and may not function correctly.  
> Avoid selecting files that may be compiled or executable text files, images, or any other types of files that cannot be read conventionally.  
> Running a comparison on two files of any of the types mentioned above may increase the memory usage of the application and could crash your computer. Please proceed with caution.

>[!NOTE]  
> Please note that any issues, bugs, suggestions, or comments are always welcome and greatly appreciated.

## [0.1.0] - 2024-10-28

Initial release. Some features have been postponed or deferred to a future release. However there are already enough features to make it useful.</br>
This version of FileCheckerApp is an MVP.

### Added

* Añadido servicio de comparación de directorios.
* Añadida función que permite analizar los directorios.
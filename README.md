# Dynamic Console Date Display

## Overview

Dynamic Console Date Display is a console application written in C#. It displays the current date and time in a specified format and updates this display at a defined interval. The format and the update interval are set in a configuration file, which the program monitors for changes. If the configuration file changes, the application automatically updates its behavior accordingly without needing to be restarted.

This project demonstrates the use of file monitoring, JSON configuration management, and dynamic settings in a simple C# application.

## Features

- **Dynamic Configuration**: Change the date/time format and update interval without restarting the application.
- **File Monitoring**: Automatically detect and apply changes made to the configuration file.
- **Error Handling**: Includes basic error handling for file access issues and JSON parsing.

## Configuration

The application behavior is configured through a `config.json` file located in the application's root directory. This file should follow the following structure:

```json
{
  "DateFormat": "yyyy-MM-dd HH:mm:ss",
  "IntervalInSeconds": 1
}

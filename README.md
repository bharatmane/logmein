# LogMeIn

## Overview
LogMeIn is an automated login tool designed to streamline the login process for specified websites. The application securely stores and retrieves user credentials, automates login actions, and handles file downloads and execution.

## Features
- Automated login to specified websites.
- Secure storage and retrieval of user credentials.
- Automated file download and opening process.
- Easy-to-use interface with minimal setup required.

## Requirements
- .NET 6.0 SDK or later
- Google Chrome browser
- ChromeDriver (compatible with your Chrome version)

## Installation

### Using the Installer
1. Download the installer from the [releases page](https://github.com/yourusername/logmein/releases).
2. Run the installer and follow the on-screen instructions to complete the installation.

### Manual Installation
1. Ensure that .NET 6.0 SDK or later is installed on your system. Download from [here](https://dotnet.microsoft.com/download).
2. Install Google Chrome if it is not already installed. Download from [here](https://www.google.com/chrome/).
3. Download and install ChromeDriver, matching your Chrome version. Download from [here](https://sites.google.com/chromium.org/driver/).
4. Place the ChromeDriver executable in a directory included in your system's PATH environment variable.

## Usage
1. Run the application by executing the `LogMeIn.exe` file.
2. On the first run, you will be prompted to enter your User ID, Password, and PIN.
3. The credentials will be securely stored for future use.
4. The application will automate the login process and handle file downloads.

## Updating ChromeDriver
To ensure compatibility with the latest version of Google Chrome, you may need to update ChromeDriver:
1. Download the latest ChromeDriver from [here](https://sites.google.com/chromium.org/driver/).
2. Replace the existing ChromeDriver executable in the directory included in your system's PATH environment variable.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Support
For support or to report issues, please contact [Your Email or Support Contact].

## Contributing
Contributions are welcome! Please fork the repository and submit a pull request.

## Acknowledgements
- [Selenium](https://www.selenium.dev/)
- [ChromeDriver](https://sites.google.com/chromium.org/driver/)

Thank you for using LogMeIn!

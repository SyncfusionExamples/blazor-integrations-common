---
title: Blazor-React Integration | Syncfusion
label: blazor-react-syncfusion
description: A minimal sample showing how to embed a Syncfusion Blazor DataGrid (hosted by a Blazor Server app) inside a React application built with Vite. Includes proxy configuration to forward Blazor static assets and SignalR endpoints from the Blazor host to the React dev server.
---

# Integrating Blazor Components (Syncfusion DataGrid) into React + Vite

## Overview

This project demonstrates embedding a Blazor Server component (Syncfusion Blazor DataGrid) inside a React application built with Vite.

## Prerequisites

* [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet)
* [Node.js 18 or later](https://nodejs.org/en/download/)

## Project Structure

- BlazorServerHost — Blazor Server app that hosts the Syncfusion DataGrid component
- react-grid — React + Vite app that embeds the Blazor UI

## Running the project

1. Start Blazor Server
    - Open a terminal, navigate to the Blazor server folder and run:

    ```
    dotnet restore
    dotnet run
    ```
   - Keep this terminal running. The app will print the listening URL (e.g. http://localhost:5000). Note this URL and update the `target` fields in `react-grid/vite.config.js` proxy configuration to match it.

2. Start the React (Vite) app
   - Open a new terminal, navigate to the React app folder and run:

     ```
     npm install
     npm run dev
     ```

   - Vite will show the dev server URL (default http://localhost:5173). Open it in your browser.

3. Verify integration
   - With the Blazor server running and the React dev server open in your browser, the React page should load and display the embedded Blazor DataGrid content.

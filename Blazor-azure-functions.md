--- 
layout: post
title: Blazor with Azure Functions | Syncfusion
description: Step-by-step guide to use Azure Functions as a serverless backend for Blazor WebAssembly with Syncfusion components (Grid, Scheduler, DatePicker, Toast).
platform: Blazor
control: Common
documentation: ug
---

# Blazor with Azure Functions

This guide shows how to build a runnable Blazor WebAssembly app that uses Azure Functions as a serverless backend and integrates Syncfusion Blazor components (`SfGrid`, `SfSchedule`, `SfDatePicker`, `SfToast`). It focuses on practical steps for local development, security options (Function keys and Entra ID / EasyAuth), calling functions from Blazor, CORS, error/retry handling, and a compact example (Orders list + Scheduler).

## Why use Azure Functions with Blazor?

Azure Functions are serverless, cost-efficient, and scale independently from the UI, making them ideal for small API surfaces backing a Blazor client. The pairing reduces operational overhead while enabling rapid UI and API iteration, and it suits common scenarios like CRUD APIs, webhooks, and lightweight integration layers.

## Prerequisites

You need an Azure subscription, .NET 10 SDK, Azure Functions Core Tools, Azure CLI, Visual Studio or VS Code, and a Syncfusion license key (register in `Program.cs`). Optionally create an Entra ID app registration if you plan to use token-based authentication for user-level access.

## Secure Azure Functions

Protect endpoints using Function keys for simple, quick authorization or Microsoft Entra ID for token-based, per-user authorization and auditing. EasyAuth lets Azure validate tokens for you; validate JWTs in function code when you need custom claims or fine-grained control.

### Function-level authorization

Function keys are a shared-secret model and are sent as `?code={functionKey}` or via header `x-functions-key`. They are easy to use for trusted callers and prototypes but are not suitable for per-user access control in production.

### EasyAuth / Entra ID

Register an application in Entra ID and configure the Function App Authentication provider (EasyAuth) to require tokens, or keep EasyAuth off and validate JWTs inside functions with Microsoft.IdentityModel libraries. For production, Entra ID and managed identities provide better security and auditability than function keys.

## Calling Azure Functions from Blazor

Blazor WebAssembly runs in the browser and calls Functions over HTTPS, so you must configure CORS and use `HttpClient` with either a function key or a bearer token. For Blazor Server, prefer server-to-server flows (managed identity or confidential client) to avoid exposing tokens to the browser.

## Using HttpClient with Auth Headers

Register `HttpClient` in the Blazor `Program.cs` and, when using Entra ID, acquire tokens with MSAL (`AddMsalAuthentication`) and `IAccessTokenProvider`. Attach `Authorization: Bearer {token}` to requests before calling protected Function endpoints.

## Enabling CORS

Configure CORS on the Function App (Azure Portal → API → CORS) and whitelist the exact Blazor origin(s) used in development and production (e.g., `https://localhost:5001`). Do not use `*` in production to avoid exposing endpoints to arbitrary origins.

## Working with Function Apps in a Real‑World Blazor App

This sample exposes `GET /api/orders` and `POST /api/orders`. The Blazor page uses `SfDatePicker` to select date ranges, `SfGrid` to list orders, `SfSchedule` to show order events, and `SfToast` for notifications. Keep functions single-purpose, persist real data in storage, and enable Application Insights for telemetry.

## Error & Retry Handling

Distinguish 4xx client errors from transient 5xx or network failures. Retry transient failures with exponential backoff and surface user-friendly messages in the UI using `SfToast`. Use Durable Functions or queues for long-running or guaranteed processing.

## Best Practices

Prefer Entra ID and managed identities in production, whitelist CORS origins, store secrets in Key Vault, and enable Application Insights. For Syncfusion controls, lazy-load scripts/styles and use virtualization or paging for large datasets to keep UI responsive.

## Azure function with Blazor Components example

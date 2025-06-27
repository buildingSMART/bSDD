#!/usr/bin/env python3
"""
Azure B2C Authentication Script for Desktop Applications
This script demonstrates how to authenticate with Azure B2C using MSAL Python
"""

import json
import sys
import webbrowser
from msal import PublicClientApplication
import requests

# Azure B2C Configuration
# Replace these with your actual B2C tenant and application details
B2C_TENANT_NAME = "buildingsmartservices"
B2C_TENANT_ID = f"{B2C_TENANT_NAME}.onmicrosoft.com"
CLIENT_ID = "your-client-id"  # Application (client) ID from Azure portal
AUTHORITY = f"https://authentication.buildingsmart.org/{B2C_TENANT_ID}/B2C_1a_signupsignin_c"
REDIRECT_URI = "http://localhost:8080"  # Must match what's configured in Azure

# Scopes - replace with your API scopes or use default
SCOPES = [
    f"https://{B2C_TENANT_ID}/bsddapi/read"
]

def create_app():
    """Create MSAL PublicClientApplication instance"""
    return PublicClientApplication(
        client_id=CLIENT_ID,
        authority=AUTHORITY,
        # token_cache=...  # Optional: add token cache for persistence
    )

def get_token_interactive(app):
    """
    Acquire token interactively (opens browser)
    This is the main authentication flow for desktop apps
    """
    try:
        # First, check if we have a cached token
        accounts = app.get_accounts()
        if accounts:
            print(f"Found {len(accounts)} account(s) in cache")
            # Try to get token silently for the first account
            result = app.acquire_token_silent(SCOPES, account=accounts[0])
            if result:
                print("Token acquired silently from cache")
                return result
        
        # If no cached token, acquire interactively
        print("No cached token found. Starting interactive authentication...")
        result = app.acquire_token_interactive(
            scopes=SCOPES,
            redirect_uri=REDIRECT_URI,
            # Optional parameters:
            # login_hint="user@example.com",  # Pre-fill username
            # prompt="login",  # Force login even if user is signed in
        )
        
        return result
        
    except Exception as e:
        print(f"Error during authentication: {e}")
        return None

def get_user_info(access_token):
    """
    Example: Use the access token to call an API
    This demonstrates how to use the token after authentication
    """
    headers = {
        'Authorization': f'Bearer {access_token}',
        'Content-Type': 'application/json'
    }
    
    # Example API call - replace with your actual API endpoint
    try:
        # Microsoft Graph example (if you have the right scopes)
        response = requests.get(
            'https://graph.microsoft.com/v1.0/me',
            headers=headers
        )
        
        if response.status_code == 200:
            return response.json()
        else:
            print(f"API call failed: {response.status_code} - {response.text}")
            return None
            
    except requests.exceptions.RequestException as e:
        print(f"Error calling API: {e}")
        return None

def main():
    """Main authentication flow"""
    print("Azure B2C Desktop Authentication Demo")
    print("=" * 40)
    
    # Validate configuration
    if "your-tenant-name" in B2C_TENANT_NAME or "your-client-id" in CLIENT_ID:
        print("❌ Please update the configuration variables at the top of the script!")
        print("   - B2C_TENANT_NAME: Your B2C tenant name")
        print("   - CLIENT_ID: Your application's client ID")
        print("   - SCOPES: Your API scopes")
        sys.exit(1)
    
    # Create MSAL app
    app = create_app()
    print(f"🔧 Configured for tenant: {B2C_TENANT_ID}")
    print(f"🔧 Client ID: {CLIENT_ID}")
    print(f"🔧 Redirect URI: {REDIRECT_URI}")
    
    # Authenticate
    result = get_token_interactive(app)
    
    if result and "access_token" in result:
        print("✅ Authentication successful!")
        print(f"🔑 Access token acquired (expires in {result.get('expires_in', 'unknown')} seconds)")
        
        # Display user information from the token
        if "id_token_claims" in result:
            claims = result["id_token_claims"]
            print(f"👤 User: {claims.get('name', 'N/A')}")
            print(f"📧 Email: {claims.get('emails', ['N/A'])[0] if claims.get('emails') else 'N/A'}")
        
        # Example: Use the token to call an API
        print("\n🌐 Testing API call with access token...")
        user_info = get_user_info(result["access_token"])
        if user_info:
            print("✅ API call successful!")
            print(f"   Response: {json.dumps(user_info, indent=2)}")
        
        # Save token for inspection (optional)
        with open("token_response.json", "w") as f:
            # Remove sensitive data before saving
            safe_result = {k: v for k, v in result.items() if k != "access_token"}
            json.dump(safe_result, f, indent=2)
            print("💾 Token response saved to token_response.json (access token excluded)")
        
    else:
        print("❌ Authentication failed!")
        if result:
            print(f"   Error: {result.get('error', 'Unknown error')}")
            print(f"   Description: {result.get('error_description', 'No description')}")
        sys.exit(1)

if __name__ == "__main__":
    main()

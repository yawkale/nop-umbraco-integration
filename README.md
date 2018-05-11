# nop-umbraco-integration
Integration of nopCommerce and Umbraco

Umbraco demo site:
http://umbraco.nopintegra.com

Umbraco backoffice login:
username: test
password: test1234


customer login:
test.me@test.com
T12345678

Nop installation, should be used only like backend system:
http://nop.nopintegra.com


Installation:

# nopCommerce site configuration
1. Install api plugin for nopcommerce  https://www.nopcommerce.com/p/2464/api-plugin.aspx
2. Open api configuration - /Plugins/ManageClientsAdmin/List 
3. Add new client ![addclient](https://user-images.githubusercontent.com/10168594/31589105-58f761f4-b204-11e7-909d-64763add59b4.png)


# Umbraco site configuration - starter kit
1. Install Starter Kit
2. Change client details - from nopCommerce site
![umbraco_config](https://user-images.githubusercontent.com/10168594/31589150-55254b08-b205-11e7-8dcb-a86f43ae44f9.png)


Basic Client configuration
    <add key="ClientId" value="6eb94fe0-5347-4725-87dc-0d2ec8bdb8a7" />
    <add key="ClientSecret" value="1156bf6c-7e66-40d2-bf86-592cfd0364af" />
    <add key="ServerUrl" value="http://nop.nopintegra.com/" />
    <add key="RedirectUrl" value="http://localhost:64146/umbraco/surface/Authorization/GetAccessToken" />
    
Multi-store configuration
    <add key="NopStoreId" value="1" />
    <add key="CreateProductLimitToStore" value="false" />
    <add key="GetProductLimitToStore" value="false" />
    


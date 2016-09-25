#region copyright
// Copyright 2016 Digital Receipt Exchange Limited
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
#endregion
package net.dreceiptx.receipt.config;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.URISyntaxException;
import java.net.URL;
import java.util.Properties;

/**
 * Simple Properties based ConfigManager
 */
public class PropertiesConfigManager implements ConfigManager{
    private Properties _properties;
    private final String _configFile;
    private static final String _defaultConfigFile = "./src/main/resources/config/drx.properties";

    /**
     * Creates instanc of PropertiesConfigManager with the default config file
     * location ./config/drx.properties
     */
    public PropertiesConfigManager() {
        this(_defaultConfigFile);
    }

    /**
     * Creates instance of the PropertiesConfigManager with the properties file
     * read from the configFileLocations
     * @param configFileLocation the location to read the Properties file from.
     */
    public PropertiesConfigManager(String configFileLocation) {
        _configFile = configFileLocation;
        _properties = getdRxConfig();
    }

    public String getConfigValue(String key) {
        return _properties.getProperty(key);
    }

    public boolean exists(String key) {
        return _properties.containsKey(key);
    }

    private Properties getdRxConfig() {
        Properties properties = new Properties();
        try {
            properties.load(new FileInputStream(_configFile));
        } catch (Exception e) {
            //TODO: Add error handling
        }
        return properties;
    }

    public void setConfigValue(String key, String value) {
        _properties.setProperty(key, value);
    }
    
    public void setConfigValue(String key, String value, boolean commit) {
        this.setConfigValue(key, value);
        if(commit){
            try {
                URL url = PropertiesConfigManager.class.getResource(_configFile);
                _properties.store(new FileOutputStream(new File(url.toURI())), null);
            } catch (IOException e) {
                //TODO: Add error handling
            } catch (URISyntaxException e) {
                //TODO: Add error handling
            }
        }
    }
}

/*
 * Copyright 2016 Digital Receipt Exchange Limited
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package net.dreceiptx.receipt.serialization.json;

import net.dreceiptx.users.NewUser;
import net.dreceiptx.users.UserConfigOptionType;
import net.dreceiptx.users.UserIdentifierType;
import com.google.gson.JsonArray;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;

import java.lang.reflect.Type;
import java.util.List;
import java.util.Map;

public class NewUsersSerializer implements JsonSerializer<List<NewUser>>
{   
    @Override
    public JsonElement serialize(List<NewUser> newUsers, Type type, JsonSerializationContext jsc) {
        JsonObject tree = new JsonObject();
        tree.addProperty("usersToRegister", newUsers.size());
        JsonArray users = new JsonArray();
        
        for (NewUser newUser : newUsers) {
            JsonObject userObject = new JsonObject();
            userObject.addProperty("userEmail", newUser.getEmail());
            JsonArray identifiersArray = new JsonArray();
            for (Map.Entry<UserIdentifierType, String> entry : newUser.getIdentifiers().entrySet()) {
                JsonObject userIdentifierObject = new JsonObject();
                userIdentifierObject.addProperty("type", entry.getKey().getValue());
                userIdentifierObject.addProperty("value", entry.getValue());
                identifiersArray.add(userIdentifierObject);
            }
            userObject.add("identifiers", identifiersArray);
            JsonArray configArray = new JsonArray();
            for (Map.Entry<UserConfigOptionType, String> entry : newUser.getConfig().entrySet()) {
                JsonObject configObject = new JsonObject();
                configObject.addProperty("option", entry.getKey().getValue());
                configObject.addProperty("value", entry.getValue());
                configArray.add(configObject);
            }
            userObject.add("config", configArray);
            users.add(userObject);
        }
        
        tree.add("users", users);
        
        return tree;
    }
}

/*
    Data service module with ajax calls to the server
*/
define("user/user.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get items
                    amplify.request.define('getUsers', 'ajax', {
                        url: ist.siteUrl + '/Api/User/GetAllUsers',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'GET'
                    });

                    // Define request to get items
                    amplify.request.define('addUser', 'ajax', {
                        url: ist.siteUrl + '/Api/User/Add',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'POST'
                    });

                    // Define request to get items
                    amplify.request.define('updateUser', 'ajax', {
                        url: ist.siteUrl + '/Api/User/Update',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'PUT'
                    });


                    // Define request to get items
                    amplify.request.define('deleteUser', 'ajax', {
                        url: ist.siteUrl + '/Api/User/Delete',
                        dataType: 'json',
                        decoder: amplify.request.decoders.istStatusDecoder,
                        type: 'DELETE'
                    });


                    isInitialized = true;
                }
            },
            // Get base data
            getUsers = function (callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getUsers',
                    success: callbacks.success,
                    error: callbacks.error,
                });
            },
            addUser = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'addUser',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            updateUser = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'updateUser',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            },
            deleteUser = function (param, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'deleteUser',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: param
                });
            };

        return {
            getUsers: getUsers,
            addUser: addUser,
            updateUser: updateUser,
            deleteUser: deleteUser
        };
    })();

    return dataService;
});
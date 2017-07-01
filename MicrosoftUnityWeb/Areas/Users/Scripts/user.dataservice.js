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
            };

        return {
            getUsers: getUsers,
        };
    })();

    return dataService;
});
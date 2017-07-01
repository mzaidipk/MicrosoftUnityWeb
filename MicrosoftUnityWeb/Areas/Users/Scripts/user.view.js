define("user/user.view", ["jquery", "amplify", "ko", "user/user.dataservice"],
    function ($, amplify, ko, dataservice) {


        dataservice.getUsers({
            success: function (data) {
                    console.log(data);
                }
            ,
            error: function (response) {
                //pageHasErrors(true);
                //toastr.error("Failed to load base data. Error: " + response, "Please Reload", ist.toastrOptions);
                console.log(response);
            }
        });


		function AppViewModel() {
			this.firstName = ko.observable("Bert");
			this.lastName = ko.observable("Bertington");

			this.fullName = ko.computed(function() {
				return this.firstName() + " " + this.lastName();    
			}, this);

			this.capitalizeLastName = function() {
				var currentVal = this.lastName();
				this.lastName(currentVal.toUpperCase());
			};    
		}

		// Activates knockout.js
		ko.applyBindings(new AppViewModel());
});
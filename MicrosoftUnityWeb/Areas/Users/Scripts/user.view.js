define("user/user.view", ["jquery", "amplify", "ko", "user/user.dataservice"],
    function ($, amplify, ko, dataservice) {
        
        var userModel = function (userId, firstName, lastName) {
            this.userId = userId;
            this.firstName = firstName;
            this.lastName = lastName;
        }
        
        function  UserViewModel()
        {
            var self = this;
            //self.userViewData = ko.observableArray([new userModel(0, 'asd', 'asd')]);
            self.userViewData = ko.observableArray([]);
            


            dataservice.getUsers({
                success: function (data) {
                    //userdata = data;
                    //self.userViewData = ko.observableArray(userdata);
                    console.log('data is ', data);

                    ko.utils.arrayPushAll(self.userViewData(), data);
                    console.log('userdata ', self.userViewData());
                },
                error: function (response) {
                    //pageHasErrors(true);
                    //toastr.error("Failed to load base data. Error: " + response, "Please Reload", ist.toastrOptions);
                    console.log(response);
                }
            });

            
            
            //self.userViewData = ko.observableArray([{UserId:1, FirstName:"asd", LastName:"asd"}]);
        }

        ko.applyBindings(new UserViewModel());

		//function AppViewModel() {
		//	this.firstName = ko.observable("Bert");
		//	this.lastName = ko.observable("Bertington");

		//	this.fullName = ko.computed(function() {
		//		return this.firstName() + " " + this.lastName();    
		//	}, this);

		//	this.capitalizeLastName = function() {
		//		var currentVal = this.lastName();
		//		this.lastName(currentVal.toUpperCase());
		//	};
		//}

		// Activates knockout.js
		//ko.applyBindings(new AppViewModel());
});
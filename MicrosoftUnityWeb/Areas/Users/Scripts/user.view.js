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
            self.addOrEdit = ko.observable(false);

            //self.userViewData = ko.observableArray([new userModel(0, 'asd', 'asd')]);
            self.userViewData = ko.observableArray([]);

            dataservice.getUsers({
                success: function (data) {
                    console.log('data is ', data);
                    // TO ASSIGN VALUES TO VARIABLE OF TYPE KO YOU PASS THEM AS PARAMETERS
                    self.userViewData(data);
                    //ko.utils.arrayPushAll(self.userViewData, data);
                },
                error: function (response) {
                    //pageHasErrors(true);
                    //toastr.error("Failed to load base data. Error: " + response, "Please Reload", ist.toastrOptions);
                    console.log(response);
                }
            });
            
            // Operations
            self.addNewUser = function () {
                self.addOrEditRow(new userModel());
                self.addOrEdit(true);
            }

            self.close = function () {
                console.log('Closing');
                self.addOrEdit(false);
                //self.addOrEditRow.dispose();
            }
            
            self.addOrEditRow = ko.observable();

            //self.addOrEditRow = ko.observable(new userModel());
            //self.addNewUser = function () {
            //    self.userViewData.push(new userModel(-1, "", ""));
            //}
            //self.removeUser = function (seat) { self.seats.remove(seat) }

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
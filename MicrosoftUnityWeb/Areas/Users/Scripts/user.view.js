define("user/user.view", ["jquery", "amplify", "ko", "user/user.dataservice"],
    function ($, amplify, ko, dataservice) {
        
        var userModel = function (userId, firstName, lastName) {
            this.userId = userId;
            this.firstName = firstName;
            this.lastName = lastName;
        }
        
        var userModelEdit = function (userId, firstName, lastName) {
            this.userId = ko.observable(userId);
            this.firstName = ko.observable(firstName);
            this.lastName = ko.observable(lastName);
        }

        function  UserViewModel()
        {
            //Varaibles
            var self = this;
            self.addOrEdit = ko.observable(false);
            self.addOrEditRow = ko.observable();
            self.userViewData = ko.observableArray([]);

            dataservice.getUsers({
                success: function (data) {
                    console.log('data is ', data);
                    // TO ASSIGN VALUES TO VARIABLE OF TYPE KO YOU PASS THEM AS PARAMETERS
                    self.userViewData(data);
                    //ko.utils.arrayPushAll(self.userViewData, data);
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

            self.save = function(userModelParamSave)
            {
                console.log("start saving", userModelParamSave);
                if (userModelParamSave.userId < 0 || userModelParamSave.userId === undefined || userModelParamSave.userId == null)
                {
                    dataservice.addUser(userModelParamSave, {
                        success: function (data) {
                            console.log('data is ', data);
                            //userModelParamSave.userId = 5;
                            //console.log('data is ', userModelParamSave);
                            self.userViewData.push(userModelParamSave);
                            //ko.utils.arrayPushAll(self.userViewData, userModelParamSave);
                        },
                        error: function (response) {
                            console.log(response);
                        }
                    });
                }
                else
                {
                    dataservice.updateUser(userModelParamSave, {
                        success: function (data) {
                            console.log('data is ', data);
                            //userModelParamSave.userId = 5;
                            //console.log('data is ', userModelParamSave);
                            //self.userViewData.push(userModelParamSave);
                            //ko.utils.arrayPushAll(self.userViewData, userModelParamSave);
                        },
                        error: function (response) {
                            console.log(response);
                        }
                    });
                }
            }

            self.EditUser = function (editUser) {
                //var temp = ko.observable(editUser);
                self.addOrEditRow(editUser);
                self.addOrEdit(true);
            }

            self.DeleteUser = function (deleteUserParam) {
                dataservice.deleteUser(deleteUserParam, {
                        success: function (data) {
                            console.log('data is ', data);
                        },
                        error: function (response) {
                            console.log(response);
                        }
                    });
            }

            self.close = function () {
                console.log('Closing');
                self.addOrEdit(false);
                //self.addOrEditRow.dispose();
            }
            
            

            
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
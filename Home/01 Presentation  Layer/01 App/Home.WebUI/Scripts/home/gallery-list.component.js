angular.
  module('homeApp').
  component('galleryList', {
      templateUrl: '/views/gallery-list.template.html',
      controller: ['$http', function PhoneListController($http) {
          var self = this;
        //  this.phones = [{ "Name": "TimeLine", "Count": 85 }, { "Name": "EquipmentMake", "Count": 5 }, { "Name": "RawFormat", "Count": 2 }, { "Name": "Selfie", "Count": 36 }];
          //$http.get('http://127.0.0.1:9000/api/gallery').then(function (response) {
          //    self.phones = response.data;
          //});
          $http({
              method: 'get',
              url: 'http://127.0.0.1:9000/api/gallery',
              headers: { 'Authorization': 'Basic admin' }  //请求头里会添加Authorization属性为'code_bunny'
          }).then(function (response) {
              console.log(response.data)
              self.phones =response. data;
          });
          self.goto = function (name) { alert(name)};
      //    this.orderProp = 'age';
      }]
  });
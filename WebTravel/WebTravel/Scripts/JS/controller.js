var app = angular
        .module('myapp', [])
        .controller('controllerIndex', function controller($scope) {
            $scope.title = 'controller';


            var types = [
                    { name: "Camp", like: 0, dislike: 0 },
                    { name: "Climb mountains", like: 0, dislike: 0 },
                    { name: "Excursion", like: 0, dislike: 0 }
            ];
            $scope.types = types;
            $scope.Flike = function (type) {
                type.like++;
            };
            $scope.Fdislike = function (type) {
                type.dislike++;
            };

            var random = function () {
                return Math.floor(Math.random() * 3);
            };
            var images = [
                "/img/amsterdam.jpg", "/img/amazona.jpg", "/img/miami.jpg"
            ];
            $scope.image = function () {
                return images[random()];
            };
        });



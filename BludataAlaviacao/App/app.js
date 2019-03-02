var app = angular.module('app', ['ngRoute', 'ui.mask']);

app.run(['$rootScope', function ($rootScope) {
    $rootScope.fade = false;

    $rootScope.modal = {
        show: false
    };

    $rootScope.ufs = [{
        'uf': 'PR',
        'nome': 'Paraná'
    },
    {
        'uf': 'SC',
        'nome': 'Santa Catarina'
    }];
}]);

app.service('rootService', function ($http, $rootScope) {

    const self = this;

    self.errorMessage = err => {

        console.error(err);

        $rootScope.fade = false;

        $rootScope.modal = {
            title: err.status,
            danger: true,
            show: true,
            message: err.data.message
        };
    };

    self.successMessage = res => $rootScope.modal = {
        title: 'Sucesso!',
        success: true,
        show: true,
        message: res.data.message
    };

    self.getAll = (url, callback) => {
        $http.get(url).then(res => {
            callback(res);
            $rootScope.fade = false;
        }, err => self.errorMessage(err));
    };

    self.get = (url, id, callback) => {

        $rootScope.fade = true;

        $http.get(`${url}/${id}`).then(res => {
            callback(res);
            $rootScope.fade = false;
        }, err => self.errorMessage(err));
    };

    self.post = (url, data, callback) => {

        $rootScope.fade = true;

        $http.post(url, data)
            .then(res => {
                callback(res);
                $rootScope.fade = false;
                self.successMessage(res);
            }, err => self.errorMessage(err));
    }

    self.put = (url, id, data, callback) => {

        $rootScope.fade = true;

        $http.put(`${url}/${id}`, data).then(res => {
            callback(res);
            $rootScope.fade = false;
            self.successMessage(res);
        }, err => self.errorMessage(err));
    };

    self.delete = (url, id, callback) => {

        $rootScope.fade = true;

        $http.delete(`${url}/${id}`).then(res => {
            callback(res);
            $rootScope.fade = false;
            self.successMessage(res);
        }, err => self.errorMessage(err));
    };
});

app.config(function ($routeProvider, $locationProvider) {
    // remove o # da url
    $locationProvider.html5Mode(false);

    $routeProvider

        //    // para a rota '/', carregaremos o template home.html e o controller 'HomeCtrl'
        //    .when('/', {
        //        templateUrl: 'app/views/home.html',
        //        controller: 'HomeCtrl',
        //    })

        //    // para a rota '/sobre', carregaremos o template sobre.html e o controller 'SobreCtrl'
        //    .when('/sobre', {
        //        templateUrl: 'app/views/sobre.html',
        //        controller: 'SobreCtrl',
        //    })

        //    // para a rota '/contato', carregaremos o template contato.html e o controller 'ContatoCtrl'
        //    .when('/contato', {
        //        templateUrl: 'app/views/contato.html',
        //        controller: 'ContatoCtrl',
        //    })

        // caso não seja nenhum desses, redirecione para a rota '/'
        .otherwise({ redirectTo: '/' });
});

app.filter('cnpj', function () {
    return function (input) {
        input = input || "";
        var out = "";

        if (input.length == 14) {
            out = input.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/gi, '$1.$2.$3/$4-$5')
        } else {
            out = input;
        }

        return out;
    };
});

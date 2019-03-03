app.controller('EmpresaCtrl', function ($rootScope, $scope, $location, $routeParams, empresaService) {
    $rootScope.activetab = $location.path();

    $scope.startForm = () => {
        if (!!$routeParams.id) {
            empresaService.get($routeParams.id, res => $scope.model = res.data);
        }
    };

    $scope.startGrid = () => empresaService.getAll(res => $scope.empresas = res.data);

    $scope.enviar = (form, model) => {
        if (!!model.idEmpresa) {
            empresaService.put(model.idEmpresa, model, res => console.log(res));
        } else {
            empresaService.post(model, res => $scope.model = {});
        }
    };

    $scope.excluir = (id, nome) => {
        if (confirm(`Gostaria de excluir a empresa ${nome}?`)) {
            empresaService.delete(id, res => console.log(res));
        }
    };
});
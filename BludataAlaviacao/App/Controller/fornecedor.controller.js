app.controller('FornecedorCtrl', function ($rootScope, $scope, $location, fornecedorService) {
    $rootScope.activetab = $location.path();

    $scope.startForm = () => {
        if (!!$routeParams.id) {
            fornecedorService.get($routeParams.id, res => $scope.model = res.data);
        }
    };

    $scope.startGrid = () => fornecedorService.getAll(res => $scope.fornecedores = res.data);

    $scope.enviar = (form, model) => {
        if (!!model.idFornecedor) {
            fornecedorService.put(model.idFornecedor, model, res => console.log(res));
        } else {
            fornecedorService.post(model, res => $scope.model = {});
        }
    };

    $scope.excluir = (id, nome) => {
        if (confirm(`Gostaria de excluir o fornecedor ${nome}?`)) {
            fornecedorService.delete(id, res => console.log(res));
        }
    };
});
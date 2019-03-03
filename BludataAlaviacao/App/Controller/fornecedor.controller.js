app.controller('FornecedorCtrl', function ($rootScope, $scope, $location, $routeParams, fornecedorService, empresaService) {
    $rootScope.activetab = $location.path();

    $scope.startForm = () => {
        if (!!$routeParams.id) {
            fornecedorService.get($routeParams.id, res => $scope.model = res.data);
        } else {
            $scope.limpar();
        }

        empresaService.getAll(res => $scope.empresas = res.data);
    };

    $scope.startGrid = () => fornecedorService.getAll(res => $scope.fornecedores = res.data);

    $scope.removeTelefone = tel => {
        var telefones = [];
        $scope.model.telefones.forEach(item => {
            if (item != tel) {
                telefones.push(item);
            }
        });

        $scope.model.telefones = telefones;
    };

    $scope.addTelefone = addtelefone => {
        $scope.model.telefones.push({
            idFornecedor: $scope.model.idFornecedor,
            numeroTelefone: addtelefone
        });
        delete $scope.telefone;
    };

    $scope.limpar = () => {
        $scope.model = { isPessoaFisica: true, telefones: [] };
    };

    $scope.enviar = (form, model) => {
        if (!!model.idFornecedor) {
            fornecedorService.put(model.idFornecedor, model, res => console.log(res));
        } else {
            fornecedorService.post(model, res => $scope.limpar());
        }
    };

    $scope.excluir = (id, nome) => {
        if (confirm(`Gostaria de excluir o fornecedor ${nome}?`)) {
            fornecedorService.delete(id, res => console.log(res));
        }
    };
});
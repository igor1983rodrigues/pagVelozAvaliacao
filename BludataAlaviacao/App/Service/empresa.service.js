app.service('empresaService', function ($http, $rootScope) {
    const url = 'api/empresa';
    this.getAll = callback => $http.get(url).then(callback);

    this.get = (id, callback) => $http.get(`${url}/${id}`).then(callback, err => $rootScope.modal = {
        danger: true,
        show: true,
        message: err.data.message
    });

    this.post = (data, callback) => $http.post(url, data).then(callback, err => $rootScope.modal = {
        danger: true,
        show: true,
        message: err.data.message
    });

    this.put = (id, data, callback) => $http.put(`${url}/${id}`, data).then(callback, err => $rootScope.modal = {
        danger: true,
        show: true,
        message: err.data.message
    });

    this.delete = (id, callback) => $http.delete(`${url}/${id}`).then(callback, err => $rootScope.modal = {
        danger: true,
        show: true,
        message: err.data.message
    });
});
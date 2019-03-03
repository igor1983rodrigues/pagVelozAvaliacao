app.service('empresaService', function (rootService) {
    const url = 'api/empresa';
    this.getAll = callback => rootService.getAll(url, callback);

    this.get = (id, callback) => rootService.get(url, id, callback);

    this.post = (data, callback) => rootService.post(url, data, callback);

    this.put = (id, data, callback) => rootService.put(url, id, data, callback);

    this.delete = (id, callback) => rootService.delete(url, id, callback);
});
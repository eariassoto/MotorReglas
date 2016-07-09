var ViewModel = function () {
    var self = this;
    self.hechos = ko.observableArray();
    self.reglas = ko.observableArray();
    self.dispositivos = ko.observableArray();
    self.error = ko.observable();

    self.newHecho = {
        Nombre: ko.observable(),
        Valor: ko.observable()
    }
    self.newDisp = {
        Nombre: ko.observable(),
        Certeza: ko.observable(),
        Propiedades: ko.observableArray()
    }
    self.newRegla = {
        TipoEvaluacion: ko.observable(),
        PropiedadEvaluacion: ko.observable(),
        ValorPropiedadEvaluacion: ko.observable(),
        PropiedadDispositivo: ko.observable(),
        ValorPropiedadDispositivo: ko.observable(),
        Certeza: ko.observable()
    }
    self.modDisp = {
        Id: ko.observable(),
        Nombre: ko.observable(),
        Certeza: ko.observable(),
        Propiedades: ko.observableArray()
    }
    self.newProp = {
        Nombre: ko.observable(),
        Valor: ko.observable()
    }

    var hechosUri = '/api/Hechos/';
    var reglasUri = '/api/Reglas/';
    var dispositivosUri = '/api/Dispositivos/';
    var propiedadesUri = '/api/PropiedadDispositivos/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAll() {
        ajaxHelper(hechosUri, 'GET').done(function (data) {
            self.hechos(data);
        });
        ajaxHelper(reglasUri, 'GET').done(function (data) {
            console.log(data);
            self.reglas(data);
        });
        ajaxHelper(dispositivosUri, 'GET').done(function (data) {
            self.dispositivos(data);
        });
    }

    self.addHecho = function (formElement) {
        var hecho = {
            Nombre: self.newHecho.Nombre(),
            Valor: self.newHecho.Valor()
        };

        ajaxHelper(hechosUri, 'POST', hecho).done(function (item) {
            self.hechos.push(item);
        });
    }

    self.addRegla = function (formElement) {
        var regla = {
            TipoEvaluacion: self.newRegla.TipoEvaluacion(),
            PropiedadEvaluacion: self.newRegla.PropiedadEvaluacion(),
            ValorPropiedadEvaluacion: self.newRegla.ValorPropiedadEvaluacion(),
            PropiedadDispositivo: self.newRegla.PropiedadDispositivo(),
            ValorPropiedadDispositivo: self.newRegla.ValorPropiedadDispositivo(),
            Certeza: self.newRegla.Certeza()
        };

        ajaxHelper(reglasUri, 'POST', regla).done(function (item) {
            self.reglas.push(item);
        });
    }

    self.addDisp = function (formElement) {
        var disp = {
            Nombre: self.newDisp.Nombre(),
            Certeza: 0,
            Propiedades: self.newDisp.Propiedades()
        };

        ajaxHelper(dispositivosUri, 'POST', disp).done(function (item) {
            item.Propiedades = ko.observableArray();
            self.dispositivos.push(item);
        });
    }

    self.addProp = function (formElement) {
        var prop = {
            Nombre: self.newProp.Nombre(),
            Valor: self.newProp.Valor(),
            IdDisp: self.modDisp.Id
        };

        ajaxHelper(propiedadesUri, 'POST', prop).done(function (item) {
            ajaxHelper(dispositivosUri, 'GET').done(function (data) {
                self.dispositivos(data);
            });
        });

    }

    self.deleteHecho = function (item) {
        ajaxHelper(hechosUri + item.Id, 'DELETE').done(function (data) {
            ajaxHelper(hechosUri, 'GET').done(function (data) {
                self.hechos(data);
            });
        });
    }

    self.deleteRegla = function (item) {
        ajaxHelper(reglasUri + item.Id, 'DELETE').done(function (data) {
            ajaxHelper(reglasUri, 'GET').done(function (data) {
                self.reglas(data);
            });
        });
    }

    self.deleteDisp = function (item) {
        ajaxHelper(dispositivosUri + item.Id, 'DELETE').done(function (data) {
            ajaxHelper(dispositivosUri, 'GET').done(function (data) {
                self.dispositivos(data);
            });
        });
    }

    self.deleteProp = function (item) {
        console.log(item.Id);
        ajaxHelper(propiedadesUri + item.Id, 'DELETE').done(function (data) {
            ajaxHelper(dispositivosUri, 'GET').done(function (data) {
                self.dispositivos(data);
            });
        });
    }

    getAll();
};

ko.applyBindings(new ViewModel());
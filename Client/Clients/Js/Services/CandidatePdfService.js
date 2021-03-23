Myapp.service
    ('CandidatePdfService', ['$http', function ($http) {

        this.GetNewPdf = function () {
            return  $http({
                method: 'GET',
                url: 'https://localhost:44313/api/CandidatePdfData/0/'
            })
                .then(
                    function Success(responce) {
                        return responce.data;
                    },
                    function error(error) {
                        return error;
                    }
                )
        }

        this.SaveCandidatePdf = function (CandidatePdf) {
            return  $http({
                method: 'POST',
                url: 'https://localhost:44313/api/CandidatePdfData/' + CandidatePdf.CandidateId.Id + '/',
                data: CandidatePdf
            })
                .then(
                    function (responce) {
                        return responce.data;
                    },
                    function (error) {
                        return error.Message;
                    }
                 )
        }

        this.GetPdfByCandidateId = function (Id) {
            return  $http({
                method: 'GET',
                url: 'https://localhost:44313/api/CandidatePdfData/' + Id +'/'
            })
                .then(
                    function Success(responce) {
                        return responce.data;
                    },
                    function error(error) {
                        return error;
                    }
                )
        }

        this.DeleteCandidatePdf = function (id) {
            return  $http({
                method: 'DELETE',
                url: 'https://localhost:44313/api/CandidatePdfData/' + id + '/'
            })
                .then(
                    function Success(responce) {
                        return responce.data;
                    },
                    function error(error) {
                        return error;
                    }
                )
        }

        this.getAllPDFs = function () {
            return $http({
                method: 'GET',
                url: 'https://localhost:44313/api/candidatepdfdata/'
            })
                .then(
                    function Success(responce) {
                        return responce.data;
                    },
                    function error(error) {
                        return error;
                    }
                )
        }

        this.searchPdf = function (text) {
            return $http({
                method: 'GET',
                url: 'https://localhost:44313/api/CandidatePdfData/FindTextSearchedPdf/' + text + '/'
            })
                .then(
                    function Success(responce) {
                        return responce.data;
                    },
                    function error(error) {
                        return error;
                    }
                )
        }   
    }]);

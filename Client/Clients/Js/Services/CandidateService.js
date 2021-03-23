Myapp.service
    ('CandidateService', ['$http', function ($http) {

        this.getNewCandidate = function () {
            return $http({
                        method: 'GET',
                        url: 'https://localhost:44313/api/Candidate/0'
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

        this.saveNewCandidate = function (Candidate) {
            return $http({
                        method: 'POST',
                        url: 'https://localhost:44313/api/Candidate/',
                        data: Candidate
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

        this.GetCandidateByEmail = function (Email) {
            return  $http({
                    method: 'GET',
                url: 'https://localhost:44313/api/Candidate/GetCandidateByEmail/' + Email + '/',
                    Accept: 'application/json',
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

        this.getCandidateByUri = function (URI) {
            return  $http({
                method: 'GET',
                url : URI
            })
                .then(
                    function success(Responce) {
                        return Responce.data;
                    },
                    function error(Error) {
                        return Error;
                    }
                )
        }

        this.getCandidateById = function (Id) {
            return  $http({
                method: 'GET',
                url: 'https://localhost:44313/api/Candidate/'+Id+'/'
            })
                .then(
                    function success(Responce) {
                        return Responce.data;
                    },
                    function error(Error) {
                        return Error;
                    }
                )
        }

        this.DeleteCandidate = function (id) {
            return  $http({
                method: 'DELETE',
                url: 'https://localhost:44313/api/Candidate/' + id + '/',
            })
                .then(
                    function success(Responce) {
                        return Responce.data;
                    },
                    function error(Error) {
                        return Error;
                    }
                )
        }

        this.getAllCandidate = function () {
            return  $http({
                method: 'GET',
                url: 'https://localhost:44313/api/candidate/'
            })
                .then(
                    function (result) {
                        return result.data;
                    },
                    function (error) {
                        return error;
                    }
                )
        }

    }]);

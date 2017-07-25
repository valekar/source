angular.module('services.breadcrumbs', []);
angular.module('services.breadcrumbs').factory('breadcrumbs', ['$rootScope', '$location', function ($rootScope, $location) {

    var breadcrumbs = [];
    var breadcrumbsService = {};

    //we want to update breadcrumbs only when a route is actually changed
    //as $location.path() will get updated imediatelly (even if route change fails!)
    $rootScope.$on('$routeChangeSuccess', function (event, current) {
        alert("route changed " + current);
        var pathElements = $location.path().split('/'), result = [], i;
        var breadcrumbPath = function (index) {
            return '/' + (pathElements.slice(0, index + 1)).join('/');
        };

        pathElements.shift();
        for (i = 0; i < pathElements.length; i++) {
            result.push({ name: pathElements[i], path: breadcrumbPath(i) });
        }
        breadcrumbs = result;
    });

    breadcrumbsService.getAll = function () {
        return breadcrumbs;
    };

    breadcrumbsService.getFirst = function () {
        return breadcrumbs[0] || {};
    };

    return breadcrumbsService;
}]);

function States() {

    return [
            { id: 'AL', value: 'Alabama' },
            { id: 'AK', value: 'Alaska' },
            { id: 'AZ', value: 'Arizona' },
            { id: 'AR', value: 'Arkansas' },
            { id: 'CA', value: 'California' },
            { id: 'CO', value: 'Colorado' },
            { id: 'CT', value: 'Connecticut' },
            { id: 'DE', value: 'Delaware' },
            { id: 'DC', value: 'District of Columbia' },
            { id: 'FL', value: 'Florida' },
            { id: 'GA', value: 'Georgia' },
            { id: 'HI', value: 'Hawaii' },
            { id: 'ID', value: 'Idaho' },
            { id: 'IL', value: 'Illinois' },
            { id: 'IN', value: 'Indiana' },
            { id: 'IA', value: 'Iowa' },
            { id: 'KS', value: 'Kansas' },
            { id: 'KY', value: 'Kentucky' },
            { id: 'LA', value: 'Louisiana' },
            { id: 'ME', value: 'Maine' },
            { id: 'MD', value: 'Maryland' },
            { id: 'MA', value: 'Massachusetts' },
            { id: 'MI', value: 'Michigan' },
            { id: 'MN', value: 'Minnesota' },
            { id: 'MS', value: 'Mississippi' },
            { id: 'MO', value: 'Missouri' },
            { id: 'MT', value: 'Montana' },
            { id: 'NE', value: 'Nebraska' },
            { id: 'NV', value: 'Nevada' },
            { id: 'NH', value: 'New Hampshire' },
            { id: 'NJ', value: 'New Jersey' },
            { id: 'NM', value: 'New Mexico' },
            { id: 'NY', value: 'New York' },
            { id: 'NC', value: 'North Carolina' },
            { id: 'ND', value: 'North Dakota' },
            { id: 'OH', value: 'Ohio' },
            { id: 'OK', value: 'Oklahoma' },
            { id: 'OR', value: 'Oregon' },
            { id: 'PA', value: 'Pennsylvania' },
            { id: 'RI', value: 'Rhode Island' },
            { id: 'SC', value: 'South Carolina' },
            { id: 'SD', value: 'South Dakota' },
            { id: 'TN', value: 'Tennessee' },
            { id: 'TX', value: 'Texas' },
            { id: 'UT', value: 'Utah' },
            { id: 'VT', value: 'Vermont' },
            { id: 'VA', value: 'Virginia' },
            { id: 'WA', value: 'Washington' },
            { id: 'WV', value: 'West Virginia' },
            { id: 'WI', value: 'Wisconsin' },
            { id: 'WY', value: 'Wyoming' },
            { id: 'AS', value: 'American Samoa' },
            { id: 'GU', value: 'Guam' },
            { id: 'MP', value: 'North Mariana Islands' },
            { id: 'PR', value: 'Puerto Rico' },
            { id: 'VI', value: 'Virgin Islands' },
            { id: 'AA', value: 'Armed Forces Americas' },
            { id: 'AE', value: 'Armed Forces Eur.,Mid.East,Africa,Canada' },
            { id: 'AP', value: 'Armed Forces Pacific' },
            { id: 'AB', value: 'Alberta' },
            { id: 'BC', value: 'British Columbia' },
            { id: 'MB', value: 'Manitoba' },
            { id: 'NB', value: 'New Brunswick' },
            { id: 'NF', value: 'Newfoundland' },
            { id: 'NT', value: 'Northwest Territories' },
            { id: 'NS', value: 'Nova Scotia' },
            { id: 'NU', value: 'Nunavut' },
            { id: 'ON', value: 'Ontario' },
            { id: 'PE', value: 'Prince Edward Island' },
            { id: 'QC', value: 'Quebec' },
            { id: 'SK', value: 'Saskatchewan' },
            { id: 'YT', value: 'Yukon Territory' },
            { id: 'FM', value: 'Federated States of Micronesia' },
            { id: 'MH', value: 'Marshall Islands' },
            { id: 'PW', value: 'Palau Island' }
    ]
}
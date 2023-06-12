<template>
    <div class="container-fluid content-container">
        <DxDataGrid id="users-data-grid" :data-source="dataUsers" key-expr="ID" :columns="UsersColumns" :show-borders="true" />
    </div>
</template>

<script>
import 'devextreme/dist/css/dx.light.css';

import DxDataGrid from 'devextreme-vue/data-grid';

export default {
    components: {
        DxDataGrid,
    },
    data() {
        return {
            dataUsers: customers,
            UsersColumns: ['Login', 'Email', 'Role'],
        };
    },
};

export const customers = [{
    ID: 1,
    Login: 'Super Mart of the West',
    Address: '702 SW 8th Street',
    Role: 'Bentonville',
    State: 'Arkansas',
    Zipcode: 72716,
    Phone: '(800) 555-2797',
    Fax: '(800) 555-2171',
    Email: 'http://www.nowebsitesupermart.com',
}, {
    ID: 2,
    Login: 'Electronics Depot',
    Address: '2455 Paces Ferry Road NW',
    Role: 'Atlanta',
    State: 'Georgia',
    Zipcode: 30339,
    Phone: '(800) 595-3232',
    Fax: '(800) 595-3231',
    Email: 'http://www.noEmaildepot.com',
}, {
    ID: 3,
    Login: 'K&S Music',
    Address: '1000 Nicllet Mall',
    Role: 'Minneapolis',
    State: 'Minnesota',
    Zipcode: 55403,
    Phone: '(612) 304-6073',
    Fax: '(612) 304-6074',
    Email: 'http://www.noEmailmusic.com',
}, {
    ID: 4,
    Login: "Tom's Club",
    Address: '999 Lake Drive',
    Role: 'Issaquah',
    State: 'Washington',
    Zipcode: 98027,
    Phone: '(800) 955-2292',
    Fax: '(800) 955-2293',
    Email: 'http://www.noEmailtomsclub.com',
}, {
    ID: 5,
    Login: 'E-Mart',
    Address: '3333 Beverly Rd',
    Role: 'Hoffman Estates',
    State: 'Illinois',
    Zipcode: 60179,
    Phone: '(847) 286-2500',
    Fax: '(847) 286-2501',
    Email: 'http://www.noEmailemart.com',
}, {
    ID: 6,
    Login: 'Walters',
    Address: '200 Wilmot Rd',
    Role: 'Deerfield',
    State: 'Illinois',
    Zipcode: 60015,
    Phone: '(847) 940-2500',
    Fax: '(847) 940-2501',
    Email: 'http://www.noEmailwalters.com',
}, {
    ID: 7,
    Login: 'StereoShack',
    Address: '400 Commerce S',
    Role: 'Fort Worth',
    State: 'Texas',
    Zipcode: 76102,
    Phone: '(817) 820-0741',
    Fax: '(817) 820-0742',
    Email: 'http://www.noEmailshack.com',
}, {
    ID: 8,
    Login: 'Circuit Town',
    Address: '2200 Kensington Court',
    Role: 'Oak Brook',
    State: 'Illinois',
    Zipcode: 60523,
    Phone: '(800) 955-2929',
    Fax: '(800) 955-9392',
    Email: 'http://www.noEmailcircuittown.com',
}, {
    ID: 9,
    Login: 'Premier Buy',
    Address: '7601 Penn Avenue South',
    Role: 'Richfield',
    State: 'Minnesota',
    Zipcode: 55423,
    Phone: '(612) 291-1000',
    Fax: '(612) 291-2001',
    Email: 'http://www.noEmailpremierbuy.com',
}, {
    ID: 10,
    Login: 'ElectrixMax',
    Address: '263 Shuman Blvd',
    Role: 'Naperville',
    State: 'Illinois',
    Zipcode: 60563,
    Phone: '(630) 438-7800',
    Fax: '(630) 438-7801',
    Email: 'http://www.noEmailelectrixmax.com',
}, {
    ID: 11,
    Login: 'Video Emporium',
    Address: '1201 Elm Street',
    Role: 'Dallas',
    State: 'Texas',
    Zipcode: 75270,
    Phone: '(214) 854-3000',
    Fax: '(214) 854-3001',
    Email: 'http://www.noEmailvideoemporium.com',
}, {
    ID: 12,
    Login: 'Screen Shop',
    Address: '1000 Lowes Blvd',
    Role: 'Mooresville',
    State: 'North Carolina',
    Zipcode: 28117,
    Phone: '(800) 445-6937',
    Fax: '(800) 445-6938',
    Email: 'http://www.noEmailscreenshop.com',
}];
</script>
<style></style>
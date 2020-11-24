export interface LoggedUser {
    user: string;
}

export interface RootObject {
    html_attributions: any[];
    result: Result;
    status: string;
}

export interface Result {
    address_components: [];
    adr_address: string;
    formatted_address: string;
    geometry: [];
    icon: string;
    id: string;
    name: string;
    place_id: string;
    plus_code: [];
    reference: string;
    scope: string;
    types: string[];
    url: string;
    utc_offset: number;
    vicinity: string;
}
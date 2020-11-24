import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';
import { Injectable } from '@angular/core';



@Injectable({
    providedIn: 'root'
})

export class CommonHelpers {

    CreateTable(tableName, data, columns , showPagination = true,showentries = true) {
        const table: any = $(tableName);
        table.DataTable({
            destroy: true,          
            "scrollCollapse": true,
            "ordering": true,
            "aaData": data,
            async: false,
            "aoColumns": columns,
            "order" : []       ,
            "bPaginate": showPagination   ,
            "bInfo" :  showentries 
        });
    }


}

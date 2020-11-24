import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { DataServiceService } from 'src/app/data-service.service';
import { Router , RouterOutlet   } from '@angular/router';
import { LocalStorageService } from 'angular-web-storage';
import { Roles as Role } from '../../shared/constants'
import { CurrentContext as _context } from '../../shared/constants'
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})

export class NavBarComponent implements OnInit {
  constructor(private dataService: DataServiceService,private router: Router, private storage: LocalStorageService,private service: SharedService) { }
  ngOnInit(): void {
debugger;

this.service.Post("ApplicantStatusChanging","");

if (_context.RoleName ==  Role.RecruiterLeadRole ||_context.RoleName == Role.RecruiterRole)
{
  $("#staffing").hide();
  $("#applicantsstaffing").hide();
  $("#client").hide();
  $("#vendor").hide();
  $("#interviewer").hide();
  $("#admin").hide();
} 
else if(_context.RoleName ==  Role.Client)
{
  $("#staffing").hide();
  $("#applicantsstaffing").hide();
  $("#client").hide();
  $("#vendor").hide();
  $("#requisition").hide();
  $("#applicantsmenu").hide();
  $("#employeesmenu").hide();
  $("#admin").hide();

}
else  if (_context.RoleName ==  Role.StaffingLeadRole)
{
  $("#requisition").hide();
  $("#applicantsmenu").hide();
  $("#employeesmenu").hide();
  $("#interviewer").hide();
  $("#admin").hide();
  
  
}
else  if (_context.RoleName == Role.SalesHead)
{
  $("#applicantsstaffing").hide();
  $("#requisition").hide();
  $("#applicantsmenu").hide();
  $("#interviewer").hide();
  $("#admin").hide();
  $("#employeesmenu").hide();
  
}
else  if (_context.RoleName == Role.StaffingRole)
{
  
  $("#requisition").hide();
  $("#applicantsmenu").hide();
  $("#employeesmenu").hide();
  $("#interviewer").hide();
  $("#client").hide();
  $("#vendor").hide(); 
  $("#admin").hide();
  
}


else  if (_context.RoleName ==  Role.EmployeeRole || _context.RoleName ==  Role.ManagerRole)
{
  $("#applicantsstaffing").hide();
  $("#applicantsmenu").hide();
  $("#interviewer").hide();
  $("#staffing").hide();
  $("#client").hide();
  $("#vendor").hide(); 
  $("#admin").hide();
}
else if(_context.RoleName ==  Role.AdminRole)
{
  $("#staffing").hide();
  $("#applicantsstaffing").hide();
  $("#client").hide();
  $("#vendor").hide();
  $("#requisition").hide();
  $("#applicantsmenu").hide();
  $("#employeesmenu").hide();
  $("#interviewer").hide();
 
}



    $('#userName').text(this.storage.get('loggedUser'));
    var CURRENT_URL = window.location.href.split("#")[0].split("?")[0],
      $BODY = $("body"),
      $MENU_TOGGLE = $("#menu_toggle"),
      $SIDEBAR_MENU = $("#sidebar-menu"),
      $SIDEBAR_FOOTER = $(".sidebar-footer"),
      $LEFT_COL = $(".left_col"),
      $RIGHT_COL = $(".right_col"),
      $NAV_MENU = $(".nav_menu"),
      $FOOTER = $("footer");

    function init_sidebar() {
      function t() {
        $RIGHT_COL.css("min-height", $(window).height());
        var e = $BODY.outerHeight(),
          a = $BODY.hasClass("footer_fixed") ? -10 : $FOOTER.height(), t = $LEFT_COL.eq(1).height() + $SIDEBAR_FOOTER.height(), o = e < t ? t : e;
        o -= $NAV_MENU.height() + a,
          $RIGHT_COL.css("min-height", o)
      }
      function o() {
        $SIDEBAR_MENU.find("li").removeClass("active active-sm"),
          $SIDEBAR_MENU.find("li ul").slideUp()
      }
      $SIDEBAR_MENU.find("a").on("click", function (e) {
        var a = $(this).parent();
        a.is(".active") ? (a.removeClass("active active-sm"), $("ul:first", a).slideUp(function () {
          t()
        }
        )) : (a.parent().is(".child_menu") ? $BODY.is("nav-sm") && (a.parent().is("child_menu") || o()) : o(), a.addClass("active"), $("ul:first", a).slideDown(function () {
          t()
        }
        ))
      }
      ),
        $SIDEBAR_MENU.find('a[href="' + CURRENT_URL + '"]').parent("li").addClass("current-page"),
        $SIDEBAR_MENU.find("a").filter(function () {
          return this.href == CURRENT_URL
        }
        ).parent("li").addClass("current-page").parents("ul").slideDown(function () {
          t()
        }
        ).parent().addClass("active")
        ,
        t()
    }
    init_sidebar();
  }
  logout(){
    this.storage.clear();
    window.location.reload();
  }
  prepareRoute(outlet: RouterOutlet) {
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData['animation'];
  }
}
    
    
  
  


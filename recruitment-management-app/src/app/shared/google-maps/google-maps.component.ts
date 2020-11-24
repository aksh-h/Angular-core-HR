import { Component, OnInit, ViewChild, ElementRef, NgZone } from '@angular/core';
import { MapsAPILoader, MouseEvent } from '@agm/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApiService } from '../api.service';
import { Result } from 'src/app/models/commonModel';
import {MatAccordion} from '@angular/material/expansion';

@Component({
  selector: 'app-google-maps',
  templateUrl: './google-maps.component.html',
  styleUrls: ['./google-maps.component.css']
})
export class GoogleMapsComponent implements OnInit {
  latitude: number;
  longitude: number;
  zoom: number;
  address: string;
  Glink: string;
  PName: string;
  gResponse: Result;
  private geoCoder;
  // Addresses;
  // panelOpenState = false;

  @ViewChild('search')
  public searchElementRef: ElementRef;

  // @ViewChild(MatAccordion) accordion: MatAccordion;

  constructor(
    private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone,
    public dialogRef: MatDialogRef<GoogleMapsComponent>,
    private apiService: ApiService
  ) { }


  ngOnInit() {
    //load Places Autocomplete
    this.mapsAPILoader.load().then(() => {
      this.setCurrentLocation();
      this.geoCoder = new google.maps.Geocoder;

      let autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement);
      autocomplete.addListener("place_changed", () => {
        this.ngZone.run(() => {
          //get the place result
          let place: google.maps.places.PlaceResult = autocomplete.getPlace();
          this.PName = place.name;

          //verify result
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }

          //set latitude, longitude and zoom
          this.latitude = place.geometry.location.lat();
          this.longitude = place.geometry.location.lng();
          this.zoom = 12;
          this.getAddress(this.latitude,this.longitude);
        });
      });
    });
  }

  // Get Current Location Coordinates
  private setCurrentLocation() {
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.zoom = 5;
        this.getAddress(this.latitude, this.longitude);
      });
    }
  }


  markerDragEnd($event: MouseEvent) {
    console.log($event);
    this.latitude = $event.coords.lat;
    this.longitude = $event.coords.lng;
    this.getAddress(this.latitude, this.longitude);
  }

  getAddress(latitude, longitude) {
    debugger;
    this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {      
      console.log(results);
      console.log(status);
      if (status === 'OK') {
        if (results[0]) {
          this.zoom = 12;
          this.address = results[0].formatted_address;
          // this.Glink = "https://www.google.com/maps/place/"+this.PName+"/@"+this.latitude+","+this.longitude+",18z";
          // this.Glink = "https://maps.googleapis.com/maps/api/place/details/json?key=AIzaSyCxyr-N6yHjiLmsFqaHRM_rbWvKgyx2y80&placeid="+results[0].place_id;

          if(results[0].place_id){
            var placeID = results[0].place_id;
            this.apiService.getURL(placeID).subscribe((data) => {
               this.gResponse = data["result"];
               this.Glink = this.gResponse.url;
            })
          }

        } else {
          window.alert('No results found');
        }
      } else {
        window.alert('Geocoder failed due to: ' + status);
      }

    });
  }

  onConfirm(): void {    
    debugger;
    var obj = {
      "address": this.address,
      "link": this.Glink
    }
    this.dialogRef.close(obj);
  }
 
  onDismiss(): void {      
    this.dialogRef.close(false);   
  } 


  // fetchAddress(){
  //   debugger;
  //   this.apiService.getAddress().subscribe((data) => {
  //     this.Addresses = data;      
  //  });
  // }

}

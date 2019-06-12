import { Component, OnInit } from '@angular/core';
//import { UserService } from '../../common/services/user.service';
import { User } from '../../common/models/user';
import { AzureToolkitService } from '../../common/services/azureToolkit.service';
import { SavedImage } from '../../common/models/SavedImage';

@Component({
    selector: 'gallery',
    templateUrl: './gallery.component.html',
    styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {
    //user: User;
    savedImages: SavedImage[] | null = null;
    //userId: string;

    //constructor(private userService: UserService) { }
    constructor(private azureToolkitService: AzureToolkitService) { }

    ngOnInit(): void {        
        //this.userService.getUser().subscribe(user => this.user = user );
        //this.userId = 'FF171EBE5A251A9C007A6EF080DDF4683B5EFFCC';
        //this.azureToolkitService.getImages(this.userId).subscribe(images => {
        this.azureToolkitService.getImages().subscribe(images => {
            this.savedImages = images;
        })
    }
}
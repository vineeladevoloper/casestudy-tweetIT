import { Routes } from '@angular/router';
import { RegisterComponent } from './Pages/User/register/register.component';
import { ListUserComponent } from './Pages/User/list-user/list-user.component';
import { LoginComponent } from './Pages/User/login/login.component';
import { EditUserComponent } from './Pages/User/edit-user/edit-user.component';
import { AdminDashboardComponent } from './Pages/User/admin-dashboard/admin-dashboard.component';
import { AboutComponent } from './Pages/about/about.component';
import { ContactComponent } from './Pages/contact/contact.component';
import { UserDashboardComponent } from './Pages/User/user-dashboard/user-dashboard.component';
import { ProfileComponent } from './Pages/User/profile/profile.component';
import { AddPostComponent } from './Pages/Post/add-post/add-post.component';
import { AllPostsComponent } from './Pages/Post/all-posts/all-posts.component';
import { PostByUserComponent } from './Pages/Post/post-by-user/post-by-user.component';
import { EditPostComponent } from './Pages/Post/edit-post/edit-post.component';
import { ViewPostComponent } from './Pages/Post/view-post/view-post.component';
import { AdminNotificationComponent } from './Pages/User/admin-notification/admin-notification.component';

export const routes: Routes = [
  { path:'',component:LoginComponent},
    { path: 'about', component: AboutComponent },
    { path: 'contact', component: ContactComponent },
    { path:'register',component:RegisterComponent},
    { path:'login',component:LoginComponent},
    { path:'user-dashboard',component:UserDashboardComponent,
    children: [
      {path:'',component:AllPostsComponent},
      { path:'edit-user/:uid',component:EditUserComponent},
      { path:'profile/:uid',component:ProfileComponent},
      { path:'edit-user/:uid',component:EditUserComponent},
      { path:'all-posts',component:AllPostsComponent},
      { path:'add-post/:uid',component:AddPostComponent},
      { path:'post-by-user/:uid',component:PostByUserComponent},
      { path:'edit-post/:pid',component:EditPostComponent},   
      { path:'view-post/:pid',component:ViewPostComponent},   
    ]
    },
    {path:'admin-dashboard',component:AdminDashboardComponent,
    children: [
        {path:'',component:AllPostsComponent},
        { path:'list-users',component:ListUserComponent },
        {path:'edit-user/:uid',component:EditUserComponent},
        {path:'profile/:uid',component:ProfileComponent},
        {path:'all-posts',component:AllPostsComponent},
        { path:'post-by-user/:uid',component:PostByUserComponent},
        { path:'view-post/:pid',component:ViewPostComponent}, 
        { path:'admin-notifications',component:AdminNotificationComponent}
      ]
    }
];

import { Injectable, UnauthorizedException } from '@nestjs/common';
import { UsersService } from '../users/users.service';
import { ConfigService } from '@nestjs/config';
import { google, Auth } from 'googleapis';
import { AuthenticationService } from '../authentication/authentication.service';
import User from '../users/user.entity';
 
@Injectable()
export class GoogleAuthenticationService {
  oauthClient: Auth.OAuth2Client;
  constructor(
    private readonly usersService: UsersService,
    private readonly configService: ConfigService,
    private readonly authenticationService: AuthenticationService
  ) {
    const clientID = this.configService.get('GOOGLE_AUTH_CLIENT_ID');
    const clientSecret = this.configService.get('GOOGLE_AUTH_CLIENT_SECRET');
 
    this.oauthClient = new google.auth.OAuth2(
      clientID,
      clientSecret
    );
  }
 
  async authenticate(token: string) {
    const tokenInfo = await this.oauthClient.getTokenInfo(token);
 
    const email = tokenInfo.email;
 
    try {
      const user = await this.usersService.getByEmail(email);
 
      return this.handleRegisteredUser(user);
    } catch (error) {
      if (error.status !== 404) {
        throw new error;
      }
 
      return this.registerUser(token, email);
    }
  }
  handleRegisteredUser(user: any) {
    throw new Error('Method not implemented.');
  }
  registerUser(token: string, email: string | undefined) {
    throw new Error('Method not implemented.');
  }
 
  // ...
}
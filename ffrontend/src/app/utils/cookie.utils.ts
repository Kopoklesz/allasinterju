// src/app/utils/cookie-utils.ts
export interface JwtPayload {
    unique_name: string;
    id: string;
    role: string;
    nbf: number;
    exp: number;
    iat: number;
    iss: string;
  }


/**
 * Retrieves the value of a specified cookie.
 * @param name The name of the cookie to retrieve.
 * @returns The value of the cookie, or null if not found.
 */

/*export function getCookie(name: string): string | null {
    const match = document.cookie.match(
        new RegExp('(?:^|; )' + name.replace(/([.$?*|{}()\[\]\\/+=^])/g, '\\$1') + '=([^;]*)')
    );
    return match ? decodeURIComponent(match[1]) : null;
  }*/
    export function getCookie(name: string): string|null {
        const nameLenPlus = (name.length + 1);
        return document.cookie
            .split(';')
            .map(c => c.trim())
            .filter(cookie => {
                return cookie.substring(0, nameLenPlus) === `${name}=`;
            })
            .map(cookie => {
                return decodeURIComponent(cookie.substring(nameLenPlus));
            })[0] || null;
    }
    
    /**
   * Sets a cookie with a specified name, value, and expiration time.
   * @param token The name of the cookie.
   * @returns The value of the cookie.
   *  Number of days until the cookie expires.
   */

    export function parseJwt(token: string):JwtPayload | null {
        console.log( token)
        const base64Url = token.split('.')[1];  // Get the second part (Payload)
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/'); // Replace URL-safe chars with standard base64
        const jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));
      
        return JSON.parse(jsonPayload);  // Parse the decoded JSON
      }
  /**
   * Sets a cookie with a specified name, value, and expiration time.
   * @param name The name of the cookie.
   * @param value The value of the cookie.
   * @param days Number of days until the cookie expires.
   */
  export function setCookie(name: string, value: string, days: number): void {
    const expires = new Date(Date.now() + days * 864e5).toUTCString();
    document.cookie = `${name}=${encodeURIComponent(value)}; expires=${expires}; path=/`;
  }
  
  /**
   * Deletes a cookie by its name.
   * @param name The name of the cookie to delete.
   */
  export function deleteCookie(name: string): void {
    setCookie(name, '', -1);
  }
  
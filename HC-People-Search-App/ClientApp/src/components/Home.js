import React, { Component } from 'react';
import { SearchData } from './SearchData'

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <header className='hcSearchHead' >
          <h1>People Search App</h1>
          <p>Type in a name, interest, address or age to search and filter this people database.</p>
        </header>
        <div>
          <SearchData></SearchData>
        </div>
      </div>
    );
  }
}

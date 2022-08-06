import React, { ReactNode } from 'react';
import { Header } from './header';
import { Sidebar } from './sidebar';

interface InternalStructureProps {
    children?: ReactNode;
}

function InternalStructure({ children }: InternalStructureProps) {
  return (
    <>
      <Header />
      <Sidebar />
      <div>
        {children}
      </div>
    </>
  );
}

export { InternalStructure };

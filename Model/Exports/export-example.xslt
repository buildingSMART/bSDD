<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
<xsl:output method="xml" indent="yes" />
<xsl:template match="/" >
<BuildingInformation>
  <Classification>
    <System>
      <Name>
        <xsl:value-of select="Domain/Name" />
      </Name>
      <Items>
          <xsl:apply-templates select="//Classifications/Classification"/>
      </Items>
    </System>
  </Classification>
</BuildingInformation>
</xsl:template>

<xsl:template match="Classification">
  <Item>
    <ID>
      <xsl:value-of select="Code"/>
    </ID>
    <Name>
      <xsl:value-of select="Name"/>
    </Name>
    <Description>
      <xsl:value-of select="Definition"/>
    </Description>
    <CountriesOfUse>
      <xsl:apply-templates select="CountriesOfUse"/>
    </CountriesOfUse>
    <Children>
      <xsl:apply-templates select="ChildClassifications/Classification"/>
    </Children>
  </Item>
</xsl:template>

<xsl:template match="CountryOfUse">
  <CountryOfUse>
    <xsl:value-of select="CountryOfUse"/>
  </CountryOfUse>
</xsl:template>

</xsl:stylesheet>
import React, { useState, useEffect } from "react";
import '../../App.css';
import adminServices from "../../Services/admin.services";

function SortProducts({
  onApplyFilters,
  initialSortType,
  initialPriceRange,
  initialSelectedCategory,
}) {
  const [sortType, setSortType] = useState(initialSortType);
  const [priceRange, setPriceRange] = useState(initialPriceRange || [0, 1000]);
  const [categories, setCategories] = useState([]);
  const [selectedCategory, setSelectedCategory] = useState(initialSelectedCategory || "");

  useEffect(() => {
    adminServices
      .getCategory()
      .then((response) => {
        setCategories(response.data);
      })
      .catch((error) => {
        console.error("Error fetching categories:", error);
      });
  }, []);

  const handlePriceRangeChange = (event) => {
    const newValue = parseInt(event.target.value);
    setPriceRange([priceRange[0], newValue]);
  };

  const handleCategoryChange = (event) => {
    setSelectedCategory(event.target.value);
  };

  const handleApplyFilters = () => {
    onApplyFilters(sortType, priceRange, selectedCategory);
  };

  return (
    <div className="sort-products">
      <h5 className="sort-title">Filter Products</h5>
      <div className="sort-options">
        <h6>Sort By</h6>
        <select onChange={(e) => setSortType(e.target.value)}>
          <option value="">Select an option</option>
          <option value="priceLowToHigh">Price: Low to High</option>
          <option value="priceHighToLow">Price: High to Low</option>
          <option value="inStock">In Stock</option>
          <option value="outOfStock">Out of Stock</option>
        </select>

        <h6>Price Range</h6>
        <input
          type="range"
          min={priceRange[0]}
          max="1000"
          value={priceRange[1]}
          onChange={handlePriceRangeChange}
        />
        <div className="price-range-labels">
          <span>INR{priceRange[0]}</span>
          <span>INR{priceRange[1]}</span>
        </div>

        <h6>Categories</h6>
        <div className="category-radio-buttons">
          {categories.map((category) => (
            <div key={`category-radio-${category.id || category.categoryName}`} className="category-radio">
              <input
                type="radio"
                id={`category-${category.id || category.categoryName}`}
                name="category"
                value={category.categoryName}
                onChange={handleCategoryChange}
                checked={selectedCategory === category.categoryName}
              />
              <label htmlFor={`category-${category.id || category.categoryName}`}>
                {category.categoryName}
              </label>
            </div>
          ))}
        </div>
        <button className="apply-filters-btn" onClick={handleApplyFilters}>
          Apply
        </button>
      </div>
    </div>
  );
}

export default SortProducts;
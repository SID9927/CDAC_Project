function Product({ product }) {
    return (
        <div className="row d-flex justify-content-center my-4">
    <div className="col-sm-6">
        <div className="card shadow-sm" style={{ width: '22rem' }}>
            <div className="text-center p-3">
                <img 
                    // src={`http://localhost:9090/FarmersMarketplace/farmer/${p.id}/image`} 
                    src={`https://farmermarketplaceaspnetcore-production.up.railway.app/farmer/${p.id}/image`} 
                    className="card-img-top rounded" 
                    alt={product.stockItem} 
                    style={{ width: '200px', height: '200px', objectFit: 'cover' }} 
                />
            </div>
            <div className="card-body text-center">
                <h5 className="card-title font-weight-bold">{product.stockItem}</h5>
                <p className="card-text text-muted">High quality and fresh stock</p>
                <a href="#" className="btn btn-primary btn-block mt-2">Add to Cart</a>
            </div>
        </div>
    </div>
</div>

    );
}

export default Product;